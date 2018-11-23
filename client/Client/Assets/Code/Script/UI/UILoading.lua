module("UILoading", package.seeall)
setmetatable(UILoading, {__index=UIWidget})

UL_TEXT = "UL_TEXT"
UL_PROC = "UL_PROC"

function new(go)
	local obj = {
	}
	setmetatable(obj, {__index=UILoading})
	obj:Init(go)
	
	return obj
end

function Init(self, go)
	UIWidget.Init(self, go)
end

function Awake(self)
	self:Install({
		UL_TEXT, UL_PROC
	})
end

-- function Start(self)
-- 	local rectTrans = self:GetRectTransform(UL_PROC)
-- 	if rectTrans then
-- 		local tween = XStaticDOTween.DOLocalRotate(rectTrans, Vector3(0, 0, 360), 1, XRotateMode.LocalAxisAdd)
-- 		if tween then
-- 			tween:SetLoops(-1, XLoopType.Incremental)
-- 		end
-- 	end
-- end

