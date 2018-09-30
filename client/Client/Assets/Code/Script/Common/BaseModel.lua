module("BaseModel", package.seeall)
setmetatable(BaseModel, {__index=LuaBehaviour})

function new(go)
	local obj = {
	}
	setmetatable(obj, {__index=BaseModel})
	obj:Init(go)
	
	return obj
end

function Init(self, go)
	self:AddLuaBehaviour(go)
end

function Release(self)
	if self.gameObject then
		GameObject.Destroy(self.gameObject)
	end
end

function Awake(self)
	if not self:CloneRuntimeAnimator() then
		ELOG("Clone runtime animator error")
	end
end

function CloneRuntimeAnimator(self)
	self.animator = self.gameObject:GetComponent(Animator)
	if self.animator then
		local run = self.animator.runtimeAnimatorController
		if run then
			self.animator.runtimeAnimatorController = GameObject.Instantiate(run)
			return true
		end
	end

	return false
end

function Play(self, name)
	if self.animator and self.gameObject.activeSelf then
		self.animator:Play(name, 0, 0)
	end
end