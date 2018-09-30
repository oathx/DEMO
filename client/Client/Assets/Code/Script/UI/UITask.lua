module("UITask", package.seeall)
setmetatable(UITask, {__index=UIWidget})

UF_CENTER 			= "UF_CENTER"
UF_LEFT				= "UF_LEFT"
UF_RIGHT 			= "UF_RIGHT"
UF_TOP 				= "UF_TOP"
UF_RETURN			= "UF_RETURN"
UF_EXECUTE			= "UF_EXECUTE"
UF_CENTER_SCREEN 	= "UF_CENTER_SCREEN"
UF_CENTER_IMAGE		= "UF_CENTER_IMAGE"
UF_MENU				= "UF_MENU"

function new(go)
	local obj = {
	}
	setmetatable(obj, {__index=UITask})
	obj:Init(go)
	
	return obj
end

function Init(self, go)
	UIWidget.Init(self, go)
end

function Awake(self)
	self:Install({
			UF_CENTER, 
			UF_LEFT, 
			UF_RIGHT, 
			UF_TOP, 
			UF_RETURN, 
			UF_EXECUTE, 
			UF_CENTER_SCREEN, 
			UF_CENTER_IMAGE,
			UF_MENU
		})

	self.centerScreen = self:AddScreen(UF_CENTER_SCREEN)
end

function Start(self)
	self:RegisterClickEvent(UF_RETURN, OnReturnClicked)
	self:RegisterClickEvent(UF_EXECUTE,OnExecuteClicked)

	local aryGroupMove = {
		{name = UF_TOP, 	axis = resmng.MoveAxis.Y, dir = true, 	speed = 0.3, wait = 0.0, offset = 100},
		{name = UF_LEFT, 	axis = resmng.MoveAxis.X, dir = false, 	speed = 0.3, wait = 0.3, offset = 150},
		{name = UF_RIGHT, 	axis = resmng.MoveAxis.X, dir = true, 	speed = 0.3, wait = 0.3, offset = 150}
	}
	self:DOGroupMove(aryGroupMove, function() 
		end)
end

function Close(self, complete)
	self:DOCanvasGroupFade(1, 0, 0.1, function() 
		if complete then
			complete()	
		end
	end)
end

function OnReturnClicked(self, evtArgs)
	PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_RETURN, {
			widget = self
		})
end

function OnExecuteClicked(self, evtArgs)
	PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_EXECUTE, {
		widget = self
	})
end


