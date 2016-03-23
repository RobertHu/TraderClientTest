// luademo2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdio.h>
#include <lua.hpp>

int resume(lua_State *L)
{
	int ret;
	lua_pushvalue(L, -2);
	lua_pushvalue(L, -2);
	lua_call(L, 1, 1);
	ret = lua_toboolean(L, -1);
	lua_pop(L, 1);
	return ret;
}


static void stackDump(lua_State *L){
	int i;
	int top = lua_gettop(L);
	for(i=1; i<=top; i++){
		int t = lua_type(L, i);
		switch(t){
		case LUA_TSTRING:{
			printf("'%s'", lua_tostring(L,i));
			break;
			 }
		case LUA_TBOOLEAN:{
			printf(lua_toboolean(L, i)? "true": "false");
			break;
						   }
		case LUA_TNUMBER:{
			printf("%g",lua_tonumber(L, i));
			break;
						 }
		default:{
			printf("%s", lua_typename(L, t));
			break;
				}
		}
		printf("  ");
	}
	printf("\n");
}


void load(lua_State *L, const char *fname, int *w, int *h)
{
	if(luaL_loadfile(L, fname) || lua_pcall(L, 0, 0, 0))
		luaL_error(L, "cannot run conifg. file: %s", lua_tostring(L, -1));
	lua_getglobal(L, "width");
	lua_getglobal(L, "height");
	if(!lua_isnumber(L, -2))
		luaL_error(L, "'widht' should be a number\n");
	if(!lua_isnumber(L, -1))
		luaL_error(L, "'height' should be a number\n");
	*w = lua_tointeger(L, -2);
	*h = lua_tointeger(L, -1);
}



int _tmain(int argc, _TCHAR* argv[])
{

	lua_State *L = luaL_newstate();
	if(luaL_loadfile(L, "coroutine_demo.lua") || lua_pcall(L, 0, 0, 0))
		luaL_error(L, "cannot run conifg. file: %s", lua_tostring(L, -1));
	lua_State *L1 = lua_newthread(L);
	lua_getglobal(L1, "foo1");
	lua_pushinteger(L1, 20);
	lua_resume(L1, L, 1);

	printf("%d\n", lua_gettop(L1));
	printf("%d\n", lua_tointeger(L1, 1));
	printf("%d\n", lua_tointeger(L1, 3));


	//int width , height;

	//load(L, "setting.lua", &width, &height);
	//printf("width=%d, height=%d", width, height);
	//lua_pushboolean(L, 1);
	//lua_pushnumber(L, 10);
	//lua_pushnil(L);
	//lua_pushstring(L, "hello");
	//stackDump(L);
	//lua_pushvalue(L, -4);
	//stackDump(L);
	//lua_replace(L,3);
	//stackDump(L);
	///*lua_settop(L, 6);
	//stackDump(L);
	//lua_remove(L, -3);
	//stackDump(L);
	//lua_settop(L,-5);
	//stackDump(L);*/
	lua_close(L);

	return 0;
}

