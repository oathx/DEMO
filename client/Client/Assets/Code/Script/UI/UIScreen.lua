module("UIScreen", package.seeall)
setmetatable(UIScreen, {__index=UIWidget})

function new(go)
	local obj = {
	}
	setmetatable(obj, {__index=UIScreen})
	obj:Init(go)
	
	return obj
end

function Init(self, go)
	UIWidget.Init(self, go)
end

function Awake(self)
	self.camera 	= self.gameObject:GetComponentInChildren(Camera)
	self.analogTV 	= self.camera.gameObject:GetComponent(CC_AnalogTV)
end


