module("MenuSystem", package.seeall)
setmetatable(MenuSystem, {__index=EventModule})

function new(szName)
	local menu = {
	}
	setmetatable(menu, {__index=MenuSystem})
	menu:Init(szName)

	return menu
end

function Init(self, szName)
	EventModule.Init(self, GUtility.HashString(szName))

	-- get menu entity
	self.entity = EntityManager.GetSingleton():GetEntity(resmng.SYS_MENU)
	self.openID = 0

	-- init player entity
	self.player = EntityManager.GetSingleton():GetEntity(resmng.SYS_PLAYER)
end

function InitMenuModelShape(self, go)
	if not go or not self.player then
		return false
	end

	local shape = self.player:CreateShape(go)
	if shape then
		local model = shape:GetModel()
		local names = resmng.RandAnimationStateNames
		
		self.randomTimerID = LuaTimer.Add(3000, resmng.RandAnimationStateTime, function()
			local count = #names
			if count > 0 then
				model:Play(names[math.random(1, count)])
			end
		end)
	end

	return true
end

function DestroyMenuModelShape(self)
	if self.player then
		self.player:DestroyShape()
	end

	if self.randomTimerID then
		LuaTimer.Delete(self.randomTimerID)
		self.randomTimerID = 0
	end
end

function Active(self)
	UISystem.GetSingleton():OpenWidget(UIStyle.MENU, function(widget)
		self.uiMenu = widget

		-- set menu default display hero
		local hero = self.entity:GetDefaultHeroID()
		if hero then
			local prop = resmng.propHeroById(hero)
			if prop then
				widget:SetShapeURL(prop.Path, prop.ShapeAsset, function(go) 
					self:InitMenuModelShape(go)
				end)
			end
		end
	end)

	-- subscribe system event
	self:SubscribeEvent(GuiEvent.EVT_MENU_MODE, OnMenuModeClicked)
	self:SubscribeEvent(GuiEvent.EVT_OPENED, 	OnWindowOpened)
	self:SubscribeEvent(GuiEvent.EVT_CLOSED, 	OnWindowClosed)
end

function Detive(self)
	self:DestroyMenuModelShape()

	UISystem.GetSingleton():CloseWidget(UIStyle.MENU, function() 
		self:OnMenuClosed()
	end)
end

function Destroy(self)
	EventModule.Destroy(self)

	-- clear all system event
	self:RemoveAllEvent()
end

function OnMenuClosed(self)
	if self.openID ~= 0 then
		local dispatcher = self:GetDispatcher()
		if dispatcher then
			local prop = resmng.propSystemById(self.openID)
			if prop then
				dispatcher:Load(prop.Path, true)
			end
		end
	end

	return true
end

function OnWindowOpened(self, evtArgs)
	if evtArgs.widget then
		
	end

	return true
end

function OnWindowClosed(self, evtArgs)
	if evtArgs.widget then
		local cfg = evtArgs.widget:GetConfigure()
		if cfg.ID == UIStyle.MENU then
			return true
		end

		local dispatcher = self:GetDispatcher()
		if dispatcher then
			local system = dispatcher:Back()
			if system then
				INFO("OnWindowClosed id=%d classname=%s", cfg.ID, cfg.uiClass)
			else

			end
		end
	end

	return true
end

function OnMenuModeClicked(self, evtArgs)
	local selFunc = {
		[resmng.PLAYMODE_TASK] = resmng.SYS_TASK
	}

	local selected = selFunc[evtArgs.mode]
	if selected then
		-- set curent select task modle id
		self.openID = selected

		-- unload menu system
		self:Detive()	
	end

	return true
end