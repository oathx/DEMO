module("LoginSystem", package.seeall)
setmetatable(LoginSystem, {__index=EventModule})

function new(szName)
	local login = {
	}
	setmetatable(login, {__index=LoginSystem})
	login:Init(szName)

	return login
end

function Init(self, szName)
	EventModule.Init(self, GUtility.HashString(szName))

	self.checkTimerID 	= 0
	self.entity 		= EntityManager.GetSingleton():GetEntity(resmng.MOD_LOGIN)
end

function Active(self)
	UISystem.GetSingleton():OpenWidget(UIStyle.LOGIN, function(widget)
		self:SubscribeEvent(GuiEvent.EVT_LOGIN, 	OnLoginEvent)
		self:SubscribeEvent(GuiEvent.EVT_ACCOUNT,	OnAccountEvent)

		-- subscribe net event
		self:SubscribeEvent(NetEvent.EVT_CHECKIN, 	OnCheckIn)
	end)
end

function Detive(self)
	if self.checkTimerID ~= 0 then
		LuaTimer.Delete(self.checkTimerID)
		self.checkTimerID = 0
	end

    UISystem.GetSingleton():Clearup()
end

function OnLoginEvent(self, evtArgs)
	if string.len(evtArgs.UserName) == 0 or string.len(evtArgs.Password) == 0 then
		local text = resmng.LangText(resmng.LG_USERNAME_ERROR)
		if text then
			UISystem.GetSingleton():MessageBox(UIStyle.PROMPT, text)
		end
	else
		local flag = XTcpServer.GetSingleton():Connected()
		if not flag then
			XTcpServer.GetSingleton():Connect()

			self.checkTimerID = LuaTimer.Add(1000, function()
				Rpc:checkIn({
						userName = evtArgs.UserName,
						password = evtArgs.Password
					})
			end)					
		end	
	end

	return true
end

function OnAccountEvent(self, evtArgs)
	local closed = {
		UIStyle.LOGIN
	}
	for idx, style in pairs(closed) do
		UISystem.GetSingleton():CloseWidget(style)
	end
	
	UISystem.GetSingleton():OpenWidget(UIStyle.ACCOUNT, function(widget) 
		if self.entity and widget then
			widget:SetUserName(self.entity:GetUserName())
			widget:SetPassword(self.entity:GetPassword())
		end
	end)

	return true
end

function OnCheckIn(self, evtArgs)
	local disptacher = self:GetDispatcher()
	if disptacher then
		disptacher:Unload(self:Name())
	end

	RootContext.GetSingleton():LoadScene(resmng.SCENE_MENU, function(name, plugin) 
		
	end)

	return true
end

