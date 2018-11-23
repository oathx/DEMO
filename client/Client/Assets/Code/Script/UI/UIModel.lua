module("UIModel", package.seeall)
setmetatable(UIModel, {__index=LuaBehaviour})

function new(go)
    local model = {
    }
    setmetatable(model, {__index=UIModel})
    model:Init(go)

    return model
end

function Init(self, go)
    self:AddLuaBehaviour(go)
    
    local aryChild = self.gameObject:GetComponentsInChildren(Transform)
    for idx=1, aryChild.Length do
        if aryChild[idx].name == "MOUNT" then
            self.mount = aryChild[idx]
            break
        end
    end

    if not self.mount then
        ERROR("Can't find mount transform")
    end
end

function SetURL(self, url, layer, complete)
    if not self.url then
        self.url = url
    end

    self.mount.gameObject.layer     	= layer

    if self.model then
        GameObject.Destroy(self.model)
    end

    XResourceManager.GetSingleton():LoadAsync(url, Object, function(obj) 
        self.model = GameObject.Instantiate(obj)
        if self.model then
			self:Invalidate(self.model, layer)
			
			if complete then
				complete(self.model)
			end
        end
    end)
end

function GetModelObject(self)
	return self.model
end

function Invalidate(self, go, layer)
	local aryChild = go:GetComponentsInChildren(Transform)
	for idx=1, aryChild.Length do
		aryChild[idx].gameObject.layer	= layer
	end

	go.transform:SetParent(self.mount, false)
	
	local animator = go:GetComponent(Animator)
	if animator then
		animator.applyRootMotion = false
	end
	
	go.transform.localPosition   = Vector3.zero
	go.transform.localScale 	 = Vector3.one
end

