#include "luv.h"

static uv_prepare_t* luv_check_prepare(lua_State* L, int index) {
  uv_prepare_t* handle = (uv_prepare_t*)luv_checkudata(L, index, "uv_prepare");
  luaL_argcheck(L, handle->type == UV_PREPARE && handle->data, index, "Expected uv_prepare_t");
  return handle;
}

static int luv_new_prepare(lua_State* L) {
  uv_prepare_t* handle = (uv_prepare_t*)luv_newuserdata(L, sizeof(*handle));
  int ret = uv_prepare_init(luv_loop(L), handle);
  if (ret < 0) {
    lua_pop(L, 1);
    return luv_error(L, ret);
  }
  handle->data = luv_setup_handle(L);
  return 1;
}

static void luv_prepare_cb(uv_prepare_t* handle) {
  lua_State* L = luv_state(handle->loop);
  luv_handle_t* data = (luv_handle_t*)handle->data;
  luv_call_callback(L, data, LUV_PREPARE, 0);
}

static int luv_prepare_start(lua_State* L) {
  uv_prepare_t* handle = luv_check_prepare(L, 1);
  int ret;
  luv_check_callback(L, (luv_handle_t*)handle->data, LUV_PREPARE, 2);
  ret = uv_prepare_start(handle, luv_prepare_cb);
  if (ret < 0) return luv_error(L, ret);
  lua_pushinteger(L, ret);
  return 1;
}

static int luv_prepare_stop(lua_State* L) {
  uv_prepare_t* handle = luv_check_prepare(L, 1);
  int ret = uv_prepare_stop(handle);
  if (ret < 0) return luv_error(L, ret);
  lua_pushinteger(L, ret);
  return 1;
}

