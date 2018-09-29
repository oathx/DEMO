#include "luv.h"

static uv_req_t* luv_check_req(lua_State* L, int index) {
  uv_req_t* req = (uv_req_t*)luaL_checkudata(L, index, "uv_req");
  luaL_argcheck(L, req->data, index, "Expected uv_req_t");
  return req;
}

static int luv_req_tostring(lua_State* L) {
  uv_req_t* req = (uv_req_t*)luaL_checkudata(L, 1, "uv_req");
  switch (req->type) {
#define XX(uc, lc) case UV_##uc: lua_pushfstring(L, "uv_"#lc"_t: %p", req); break;
  UV_REQ_TYPE_MAP(XX)
#undef XX
    default: lua_pushfstring(L, "uv_req_t: %p", req); break;
  }
  return 1;
}

static void luv_req_init(lua_State* L) {
  luaL_newmetatable (L, "uv_req");
  lua_pushcfunction(L, luv_req_tostring);
  lua_setfield(L, -2, "__tostring");
  lua_pop(L, 1);
}

// Metamethod to allow storing anything in the userdata's environment
static int luv_cancel(lua_State* L) {
  uv_req_t* req = (uv_req_t*)luv_check_req(L, 1);
  int ret = uv_cancel(req);
  if (ret < 0) return luv_error(L, ret);
  // Cleanup occurs when callbacks are ran with UV_ECANCELED status.
  lua_pushinteger(L, ret);
  return 1;
}
