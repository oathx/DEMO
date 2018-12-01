module("UIBackground", package.seeall)
setmetatable(UIBackground, {__index=UIWidget})

UB_BACKIMAGE = "UB_BACKIMAGE"

function new(go)
	local obj = {
	}
	setmetatable(obj, {__index=UILogin})
	obj:Init(go)
	
	return obj
end

function Init(self, go)
	UIWidget.Init(self, go)
end

function Awake(self)
	self:Install({
		UB_BACKIMAGE
		})
end

function Start(self)
end

function Close(self, complete)
	self:DOCanvasGroupFade(1, 0, 1, function() 
		if complete then
			complete()	
		end
	end)
end