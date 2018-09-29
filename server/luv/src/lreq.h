#ifndef LUV_LREQ_H
#define LUV_LREQ_H

#include "luv.h"

typedef struct {
  int req_ref; /* ref for uv_req_t's userdata */
  int callback_ref; /* ref for callback */
  int data_ref; /* ref for write data */
  void* data; /* extra data */
} luv_req_t;

/* Used in the top of a setup function to check the arg
   and ref the callback to an integer.
*/
static int luv_check_continuation(lua_State* L, int index);

/* setup a luv_req_t.  The userdata is assumed to be at the
   top of the stack.
*/
static luv_req_t* luv_setup_req(lua_State* L, int ref);

static void luv_fulfill_req(lua_State* L, luv_req_t* data, int nargs);

static void luv_cleanup_req(lua_State* L, luv_req_t* data);

#endif
