#include "luv.h"

typedef struct {
	lua_State* L;
	char* buf;
	int	len;
	int pos;
	int ref;
	size_t all;
} uv_buffer_t;

typedef struct {
	int size;
} uv_packet_head_t;

typedef int(*uvbuffer_parse_callback)(uv_buffer_t* ub, const char* data, size_t size);

static int luv_buffer_new(lua_State* L) {
	uv_buffer_t* ub = lua_newuserdata(L, sizeof(*ub));
	ub->buf = NULL;
	ub->pos = 0;
	ub->len = 0;
	ub->L	= L;
	ub->ref = 0;
	ub->all = 0;

	if (lua_isnumber(L, 1)) {
		size_t len = luaL_checkint(L, 1);
		if (len <= 0) {
			len = 1024;
		}

		ub->buf = (char*)malloc(len);
		ub->len = len;

		memset(ub->buf, 0, len);
	}
	else {
		lua_pop(L, 1);
		return luaL_argerror(L, 1, "data must be number");
	}

	if (lua_isfunction(L, 2)) {
		lua_pushvalue(L, 2);
		ub->ref = luaL_ref(L, LUA_REGISTRYINDEX);
	}

	luaL_getmetatable(L, "uv_buffer_t");
	lua_setmetatable(L, -2);

	return 1;
}

static uv_buffer_t* luv_check_buffer(lua_State* L, int index)
{
	uv_buffer_t* ub = (uv_buffer_t*)luaL_checkudata(L, index, "uv_buffer_t");
	if (ub == NULL)
		return luaL_argerror(L, index, "NULL");

	return ub;
}

static int luv_buffer_tostring(lua_State* L)
{
	uv_buffer_t* ub = luv_check_buffer(L, 1);
	lua_pushfstring(L, 
		"ub_buffer_t: [%d/%d]\n", ub->pos, ub->len
	);

	return 1;
}

static int luv_buffer_gc(lua_State* L) {
	uv_buffer_t* ub = luv_check_buffer(L, 1);
	if (NULL != ub->buf) {
		if (ub->ref != 0) {
			luaL_unref(L, LUA_REGISTRYINDEX, ub->ref);
		}

		free(ub->buf);
		ub->buf = NULL;
		ub->pos = 0;
		ub->len = 0;
	}

	return 0;
}

static int luv_buffer_length(lua_State* L) {
	uv_buffer_t* ub = luv_check_buffer(L, 1);
	lua_pushinteger(L, ub->pos);

	return 1;
}

static int luv_buffer_write_cb(uv_buffer_t* ub, const char* data, size_t size) {
	if (ub == NULL || ub->L == NULL)
		return 0;

	lua_rawgeti(ub->L, LUA_REGISTRYINDEX, ub->ref);
	if (lua_isfunction(ub->L, -1)) {
		lua_pushlstring(ub->L,
			data, size);
		lua_pushinteger(ub->L, size);

		if (lua_pcall(ub->L, 2, 1, 0)) {
			return lua_error(ub->L);
		}
	}

	return 0;
}

static int luv_buffer_append(lua_State* L) {
	uv_buffer_t* ub = luv_check_buffer(L, 1);
	if (ub->buf == NULL) {
		return luaL_argerror(L, 1, "must be init the buffer");
	}

	if (!lua_isstring(L, 2)) {
		return luaL_argerror(L, 2, "data must be is string");
	}

	size_t size = 0;

	const char* data = luaL_checklstring(L, 2, &size);
	memcpy(
		ub->buf + ub->pos, data, size
	);
	ub->pos += size;
	
	printf("[luv_buffer] append %d bytes\n", size);

	size_t head_size = sizeof(uv_packet_head_t);
	while (ub->pos > head_size) {
		uv_packet_head_t* head = (uv_packet_head_t*)(ub->buf);
		printf("[luv_buffer] parse head size=%d pos=%d \n", head->size, ub->pos);

		if (head->size + head_size <= ub->pos) {
			size_t pack_size = head->size + head_size;

			char* pack = malloc(pack_size);
			memcpy(pack,
				ub->buf, pack_size);
			
			luv_buffer_write_cb(ub, pack, pack_size);
			
			ub->pos -= pack_size;
			memmove(ub->buf,
				ub->buf + pack_size, ub->pos);
			ub->all += pack_size;

			printf("[luv_buffer] buffer position %d parse(%d) \n", ub->pos, ub->all);
			free(pack);
			pack = NULL;
		}
	}

	return 0;
}

static const luaL_Reg luv_buffer_methods[] = {
	{ "length", luv_buffer_length },
	{ "append", luv_buffer_append },
	{ NULL, NULL }
};

static void luv_buffer_init(lua_State* L) {
	luaL_newmetatable(L, "uv_buffer_t");
	lua_pushcfunction(L, luv_buffer_tostring);
	lua_setfield(L, -2, "__tostring");
	lua_pushcfunction(L, luv_buffer_gc);
	lua_setfield(L, -2, "__gc");
	lua_newtable(L);
	luaL_setfuncs(L, luv_buffer_methods, 0);
	lua_setfield(L, -2, "__index");
	lua_pop(L, 1);
}

static const unsigned int InitialFNV = 2166136261;
static const unsigned int FNVMultiple = 16777619;

static int luv_hash_string(lua_State* L)
{
	const char* str = luaL_checkstring(L, 1);

	unsigned int hash = InitialFNV;
	for (int i = 0; i<strlen(str); i++)
	{
		hash = hash ^ str[i];
		hash = hash * FNVMultiple;
	}

	int hv = (int)(hash & 0x7FFFFFFF);
	lua_pushinteger(L, hv);

	return 1;
}