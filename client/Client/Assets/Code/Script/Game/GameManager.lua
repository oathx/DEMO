module("GameManager", mkSingleton)

function Init(self)
    self.gsm = BaseStateMachine.new()
    self.gsm:AddState(resmng.GMS_LOGIN, self.OnLoginEnter, self.OnLoginExit, self.OnLoginEvent)
    self.gsm:AddState(resmng.GMS_MAIN, self.OnMainEnter, self.OnMainExit, self.OnMainEvent)
    self.gsm:AddState(resmng.GMS_TASK, self.OnTaskEnter, self.OnTaskExit, self.OnTaskEvent)
    self.gsm:AddState(resmng.GMS_FIGHT, self.OnFightEnter, self.OnFightExit, self.OnFightEvent)
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

function PushState(self, name)
    if self.gsm then
        self.gsm:PushState(name)
    end
end

function PopState(self, name)
    if self.gsm then
        self.gsm:PopState(name)
    end
end

-- login state enter, load login scene
function OnLoginEnter(self)
    INFO("GameManager state change, login enter")
end

-- login state exit, free login scene
function OnLoginExit(self)
    INFO("GameManager state change, exit enter")
end

-- login state, proc scene event
function OnLoginEvent(self)
end

-- main menu state
function OnMainEnter(self)
    INFO("GameManager state change, main enter")
end

function OnMainExit(self)
    INFO("GameManager state change, main exit")
end

function OnMainEvent(self)
end

-- task ui enter
function OnTaskEnter(self)
    INFO("GameManager state change, task enter")
end

function OnTaskExit(self)
    INFO("GameManager state change, task exit")
end

function OnTaskEvent(self)
    
end

-- fight enter
function OnFightEnter(self)
    INFO("GameManager state change, fight enter")
end

function OnFightExit(self)
    INFO("GameManager state change, fight exit")
end

function OnFightEvent(self)
end
