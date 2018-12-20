module("GameManager", mkSingleton)

function Init(self)
    self.gsm = BaseStateMachine.new()
    self.gsm:AddState(resmng.GMS_LOGIN, self.OnLoginEnter, self.OnLoginExit, self.OnLoginEvent)
    self.gsm:AddState(resmng.GMS_MAIN, self.OnMainEnter, self.OnMainExit, self.OnMainEvent)
end

function Startup(self)
    self.gsm:SetState(resmng.GMS_LOGIN)
end

function Shutdown(self)
    self.gsm:ClearToGlobalOrBlockState()
end

function SetState(self, name)
    if self.gsm then
        self.gsm:SetState(name)
    end
end

-- login state enter
function OnLoginEnter(self)
    RootContext.GetSingleton():LoadScene(resmng.SCENE_LOGIN, function(name, plugin) 

	end)
end

function OnLoginExit(self)

end

function OnLoginEvent(self)
end

-- main menu state
function OnMainEnter(self)
	RootContext.GetSingleton():LoadScene(resmng.SCENE_MENU, function(name, plugin) 

	end)
end

function OnMainExit(self)

end

function OnMainEvent(self)
end

