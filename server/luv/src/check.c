#include "luv.h"

static uv_check_t* luv_check_check(lua_State* L, int index) {
  uv_check_t* handle = (uv_check_t*)luv_checkudata(L, index, "uv_check");
  luaL_argcheck(L, handle->type == UV_CHECK && handle->data, index, "Expected uv_check_t");
  return handle;
}

static int luv_new_check(lua_State* L) {
  uv_check_t* handle = (uv_check_t*)luv_newuserdata(L, sizeof(*handle));
  int ret = uv_check_init(luv_loop(L), handle);
  if (ret < 0) {
    lua_pop(L, 1);
    return luv_error(L, ret);
  }
  handle->data = luv_setup_handle(L);
  return 1;
}

static void luv_check_cb(uv_check_t* handle) {
  lua_State* L = luv_state(handle->loop);
  luv_handle_t* data = (luv_handle_t*)handle->data;
  luv_call_callback(L, data, LUV_CHECK, 0);
}

static int luv_check_start(lua_State* L) {
  uv_check_t* handle = luv_check_check(L, 1);
  int ret;
  luv_check_callback(L, (luv_handle_t *)handle->data, LUV_CHECK, 2);
  ret = uv_check_start(handle, luv_check_cb);
  if (ret < 0) return luv_error(L, ret);
  lua_pushinteger(L, ret);
  return 1;
}

static int luv_check_stop(lua_State* L) {
  uv_check_t* handle = luv_check_check(L, 1);
  int ret = uv_check_stop(handle);
  if (ret < 0) return luv_error(L, ret);
  lua_pushinteger(L, ret);
  return 1;
}

