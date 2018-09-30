
UIStyle = {
	LOGIN = 0,
	YES = 1,
	YESNO = 2,
	PROMPT = 3,
	ACCOUNT = 4,
	LOADING = 5,
	MENU = 6,
	TASK = 7
}

-- game ui base config table
UIConfigure = {
	[UIStyle.LOGIN] 	= {ID=UIStyle.LOGIN, 	prefab="UI/UILogin", 	uiClass="UI/UILogin", 		active=true, async=true, 	cache=true},
	[UIStyle.YES] 		= {ID=UIStyle.YES, 		prefab="UI/UIYes", 		uiClass="UI/UIYes", 		active=true, async=false, 	cache=true},
	[UIStyle.YESNO] 	= {ID=UIStyle.YESNO, 	prefab="UI/UIYesNo", 	uiClass="UI/UIYesNo", 		active=true, async=false, 	cache=true},
	[UIStyle.PROMPT] 	= {ID=UIStyle.PROMPT, 	prefab="UI/UIPrompt", 	uiClass="UI/UIPrompt", 		active=true, async=true, 	cache=false},
	[UIStyle.ACCOUNT] 	= {ID=UIStyle.ACCOUNT,	prefab="UI/UIAccount", 	uiClass="UI/UIAccount", 	active=true, async=true, 	cache=true},
	[UIStyle.LOADING] 	= {ID=UIStyle.LOADING,	prefab="UI/UILoading", 	uiClass="UI/UILoading", 	active=true, async=false, 	cache=true},
	[UIStyle.MENU] 		= {ID=UIStyle.MENU,		prefab="UI/UIMenu", 	uiClass="UI/UIMenu", 		active=true, async=false, 	cache=true},
	[UIStyle.TASK] 		= {ID=UIStyle.TASK,		prefab="UI/UITask", 	uiClass="UI/UITask", 		active=true, async=false, 	cache=true},
}

GuiEvent = {
	EVT_LOGIN 			= 10000,
	EVT_UPDATE_CHARLIST = 10001,
	EVT_SELECT_CHARITEM = 10002,
	EVT_CREATE_CHARITEM = 10003,
	EVT_ACCOUNT			= 10004,
	EVT_MENU_MODE		= 10005,
	EVT_CLOSED			= 10006,
	EVT_OPENED			= 10007,
	EVT_RETURN			= 10008,
	EVT_EXECUTE			= 10009,
}