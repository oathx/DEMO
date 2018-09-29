#include "luv.h"

typedef struct {
	char* buf;
	int	len;
	int pos;
	int max;

} uv_packet_t;

static int luv_packet_new(lua_State* L) {
	uv_packet_t* ub = lua_newuserdata(L, sizeof(*ub));
	ub->buf = NULL;
	ub->pos = 0;
	ub->len = 0;
	ub->max = 0;

	if (lua_isnumber(L, 1)) {
		ub->max = lua_tointeger(L, 1);
		ub->buf = (char*)malloc(ub->len);
	} else if (lua_isstring(L, 1)) {
		size_t size = 0;
		const char* block = luaL_checklstring(L, 1, &size);
		if (block == NULL || size <= 0) {
			return luaL_argerror(L, 2, "data is NULL");
		}
		ub->buf = malloc(size);
		memcpy(ub->buf, block, size);
		ub->len = size;
		ub->pos = 0;
		ub->max = size;
	} else {
		lua_pop(L, 1);
		return luaL_argerror(L, 1, "data must be number");
	}

	luaL_getmetatable(L, "uv_packet_t");
	lua_setmetatable(L, -2);

	return 1;
}

static uv_packet_t* luv_check_packet(lua_State* L, int index)
{
	uv_packet_t* ub = (uv_packet_t*)luaL_checkudata(L, index, "uv_packet_t");
	return ub;
}

static int luv_packet_tostring(lua_State* L)
{
	uv_packet_t* ub = luv_check_packet(L, 1);
	lua_pushfstring(L,
		"ub_packet_t: [%d/%d] max=%d \n", ub->pos, ub->len, ub->max
	);

	return 1;
}

static int luv_packet_gc(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);
	if (NULL != ub->buf) {
		free(ub->buf);
		ub->buf = NULL;
		ub->pos = 0;
		ub->len = 0;
	}

	return 0;
}

static int uvpacket_write_block(uv_packet_t* ub, const void* bytes, size_t len) {
	if (ub == NULL) {
		return 0;
	}

	int remain_len = ub->max - ub->len;
	if (remain_len <= 0) {
		return 0;
	}

	memcpy(
		ub->buf + ub->len, bytes, len
	);
	ub->len += len;

	return len;
}

static int uvpacket_read_block(uv_packet_t* ub, void* bytes, size_t len) {
	if (ub == NULL || bytes == NULL || len <= 0) {
		return 0;
	}

	if (ub->pos >= ub->len) {
		return 0;
	}

	memcpy(bytes, ub->buf + ub->pos, len);
	ub->pos += len;

	return len;
}

static int luv_packet_write_block(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	size_t size = 0;
	const char* block = luaL_checklstring(L, 2, &size);
	if (block == NULL) {
		return luaL_argerror(L, 2, "data is NULL");
	}

	size_t len = 0;
	len += uvpacket_write_block(ub, &size, sizeof(int));
	len += uvpacket_write_block(ub, block, size);

	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_byte(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int v = luaL_checkint(L, 2);
	if (v > UCHAR_MAX)
		return luaL_argerror(L, 2, "unsingned max value");

	unsigned char c = (unsigned char)v;
	int len = uvpacket_write_block(ub, &c, sizeof(unsigned char));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_short(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int v = luaL_checkint(L, 2);
	if (v > SHRT_MAX || v < SHRT_MIN)
		return luaL_argerror(L, 2, "short value error");

	short c = (short)v;
	size_t len = uvpacket_write_block(ub, &c, sizeof(short));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_ushort(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int v = luaL_checkint(L, 2);
	if (v > USHRT_MAX)
		return luaL_argerror(L, 2, "unsigned short value error");

	unsigned short c = (unsigned short)v;
	size_t len = uvpacket_write_block(ub, &c, sizeof(unsigned short));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_int(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);
	size_t len = 0;

	int v = luaL_checkint(L, 2);
	if (v > INT_MAX)
		return luaL_argerror(L, 2, "int value error");

	len = uvpacket_write_block(ub, &v, sizeof(int));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_uint(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);
	size_t len = 0;

	unsigned int v = luaL_checknumber(L, 2);
	if (v > UINT_MAX)
		return luaL_argerror(L, 2, "unsigned int value error");

	len = uvpacket_write_block(ub, &v, sizeof(unsigned int));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_float(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	float v = luaL_checknumber(L, 2);
	size_t len = uvpacket_write_block(ub, &v, sizeof(float));
	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_write_string(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	const char* v = luaL_checkstring(L, 2);
	size_t str_len = strlen(v) + 1;
	if (str_len > USHRT_MAX) {
		return luaL_argerror(L, 2, "string len > USHRT_MAX");
	}

	size_t len = 0;
	len += uvpacket_write_block(ub, &((unsigned short)str_len), sizeof(unsigned short));
	len += uvpacket_write_block(ub, v, str_len);

	lua_pushinteger(L, len);

	return 1;
}

static int luv_packet_length(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);
	lua_pushinteger(L, ub->len);
	return 1;
}

static int luv_packet_read_block(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int size = 0;
	size_t ret = uvpacket_read_block(ub, &size, sizeof(int));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read block length");
	}

	void* bytes = (void*)malloc(size);
	ret = uvpacket_read_block(ub, bytes, size);
	if (ret <= 0) {
		ub->pos -= sizeof(int);
		return luaL_argerror(L, 1, "can't read block");
	}

	lua_pushlstring(L, bytes, size);

	free(bytes);
	bytes = NULL;

	return 1;
}

static int luv_packet_read_byte(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	char c = 0;
	size_t ret = uvpacket_read_block(ub, &c, sizeof(char));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read byte");
	}

	lua_pushinteger(L, c);

	return 1;
}

static int luv_packet_read_short(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	short v = 0;
	size_t ret = uvpacket_read_block(ub, &v, sizeof(short));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read short");
	}

	lua_pushinteger(L, v);
	return 1;
}

static int luv_packet_read_ushort(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	unsigned short v = 0;
	size_t ret = uvpacket_read_block(ub, &v, sizeof(unsigned short));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read unsigned short");
	}

	lua_pushinteger(L, v);
	return 1;
}

static int luv_packet_read_int(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int v = 0;
	size_t ret = uvpacket_read_block(ub, &v, sizeof(int));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read int");
	}

	lua_pushinteger(L, v);
	return 1;
}

static int luv_packet_read_uint(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	unsigned int v = 0;
	size_t ret = uvpacket_read_block(ub, &v, sizeof(unsigned int));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read unsigned int");
	}

	lua_pushinteger(L, v);
	return 1;
}

static int luv_packet_read_float(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	float v = 0;
	size_t ret = uvpacket_read_block(ub, &v, sizeof(float));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read float");
	}

	lua_pushnumber(L, v);
	return 1;
}

static int luv_packet_read_string(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	unsigned short size = 0;
	size_t ret = uvpacket_read_block(ub, &size, sizeof(unsigned short));
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read string length");
	}

	if (size <= 0) {
		ub->pos -= sizeof(unsigned short);
		return luaL_argerror(L, 1, "can't read string");
	}

	char* bytes = (char*)malloc(size);
	ret = uvpacket_read_block(ub, bytes, size);
	if (ret <= 0) {
		return luaL_argerror(L, 1, "can't read string");
	}

	lua_pushstring(L, bytes);

	return 1;
}

static int luv_packet_to_bytes(lua_State* L) {
	uv_packet_t* ub = luv_check_packet(L, 1);

	int* len = (int*)ub->buf;
	*len = ub->len - sizeof(int);

	lua_pushlstring(L, ub->buf, ub->len);
	lua_pushinteger(L, ub->len);

	return 2;
}

static const luaL_Reg luv_packet_methods[] = {
	{ "write_block", luv_packet_write_block },
	{ "write_byte", luv_packet_write_byte },
	{ "write_short", luv_packet_write_short },
	{ "write_ushort", luv_packet_write_ushort },
	{ "write_int", luv_packet_write_int },
	{ "write_uint", luv_packet_write_uint },
	{ "write_float", luv_packet_write_float },
	{ "write_string", luv_packet_write_string },
	{ "length", luv_packet_length },
	{ "read_block", luv_packet_read_block },
	{ "read_byte", luv_packet_read_byte },
	{ "read_short", luv_packet_read_short },
	{ "read_ushort", luv_packet_read_ushort },
	{ "read_int", luv_packet_read_int },
	{ "read_uint", luv_packet_read_uint },
	{ "read_float", luv_packet_read_float },
	{ "read_string", luv_packet_read_string },
	{ "to_bytes", luv_packet_to_bytes },
	{ NULL, NULL }
};


static void luv_packet_init(lua_State* L) {
	luaL_newmetatable(L, "uv_packet_t");
	lua_pushcfunction(L, luv_packet_tostring);
	lua_setfield(L, -2, "__tostring");
	lua_pushcfunction(L, luv_packet_gc);
	lua_setfield(L, -2, "__gc");
	lua_newtable(L);
	luaL_setfuncs(L, luv_packet_methods, 0);
	lua_setfield(L, -2, "__index");
	lua_pop(L, 1);
}
