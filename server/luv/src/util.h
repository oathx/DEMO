#ifndef LUV_UTIL_H
#define LUV_UTIL_H

#include "luv.h"

#define LUV_UV_VERSION_GEQ(major, minor, patch) \
  (((major)<<16 | (minor)<<8 | (patch)) <= UV_VERSION_HEX)

void luv_stack_dump(lua_State* L, const char* name);
static int luv_error(lua_State* L, int ret);
static void luv_status(lua_State* L, int status);

#endif
