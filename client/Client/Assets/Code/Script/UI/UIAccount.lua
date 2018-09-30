module("UIAccount", package.seeall)
setmetatable(UIAccount, {__index=UIWidget})

UA_USERNAME = "UA_USERNAME"
UA_PASSWORD = "UA_PASSWORD"
UA_LOGINBTN = "UA_LOGINBTN"
UA_REGISTER = "UA_REGISTER"

function new(go)
    local widget = {
    }
    setmetatable(widget, {__index=UIAccount})
    widget:Init(go)

    return widget
end

function Init(self, go)
    UIWidget.Init(self, go)
end



function DOScale(self)
    self.gameObject.transform.localScale = Vector3.one * 0.6

    local tween = StaticDOTween.DOScale(self.gameObject.transform, 0.8, 0.5)
    if tween then
        
    end
end

function Awake(self)
    self:Install({
        UA_USERNAME, UA_PASSWORD, UA_LOGINBTN, UA_REGISTER
    })

    self:RegisterClickEvent(UA_LOGINBTN, OnLoginClicked)
    self:RegisterClickEvent(UA_REGISTER, OnRegisterClicked)
end

function SetUserName(self, userName)
    self:SetInputText(UA_USERNAME, userName)
end

function SetPassword(self, password)
    self:SetInputText(UA_PASSWORD, password)
end

function Start(self)
    self:DOScale()
end

function OnEnable(self)
    self:DOScale()
end

function OnLoginClicked(self, goSend, evtData)
    local evtArgs = {
    }

    evtArgs.UserName = self:GetInputText(UA_USERNAME)
    evtArgs.Password = self:GetInputText(UA_PASSWORD)
    
    PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_LOGIN, evtArgs)
end

function OnRegisterClicked(self, goSend, evtData)

end
