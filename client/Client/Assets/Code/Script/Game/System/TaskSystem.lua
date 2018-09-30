module("TaskSystem", package.seeall)
setmetatable(TaskSystem, {__index=EventModule})

function new(szName)
	local menu = {
	}
	setmetatable(menu, {__index=TaskSystem})
	menu:Init(szName)

	return menu
end

function Init(self, szName)
	EventModule.Init(self, 
		GUtility.HashString(szName))

	-- get task system entity
	self.entity = EntityManager.GetSingleton():GetEntity(resmng.SYS_TASK)
	if not self.entity then
		ELOG("Can't find SYS_TASK module")
	end

	self.task = self.entity:GetCurrent()
	if not self.task then
		ELOG("Can't find current task")
	end
end

function Active(self)
	UISystem.GetSingleton():OpenWidget(UIStyle.TASK, function(widget)
		self.uiTask = widget
	end)

	self:SubscribeEvent(GuiEvent.EVT_RETURN, 	OnReturnClicked)
	self:SubscribeEvent(GuiEvent.EVT_EXECUTE,	OnExecuteTask)
end

function Detive(self)
	UISystem.GetSingleton():CloseWidget(UIStyle.TASK)
end

function OnReturnClicked(self)
	self:Detive()
end

function OnExecuteTask(self, args)
	ELOG("OnExecuteTask %s %d %d", args.widget, self.task:ID(), self.task:GetSceneID())

	local sceneID = self.task:GetSceneID()
	if sceneID > 0 then
		local disptacher = self:GetDispatcher()
		if disptacher then
			disptacher:Unload(self:Name())
		end	
	
		RootContext.GetSingleton():LoadScene(sceneID, function(name, plugin)

		end)
	end
end
