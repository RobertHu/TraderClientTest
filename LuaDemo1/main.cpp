#include "luademo1.h"
#include <QtWidgets/QApplication>
#include <qdebug.h>
extern "C" {
#include <lua.h>
#include <lualib.h>
}

static void stackDump(lua_State* L){
	int i;
	int top = lua_gettop(L);
	for(i=1; i<=top; i++){
		int t = lua_type(L, i);
		switch (t)
		{
		case LUA_TSTRING:{
			printf
						 }

		}
	}
}

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	LuaDemo1 w;
	w.show();
	return a.exec();
}
