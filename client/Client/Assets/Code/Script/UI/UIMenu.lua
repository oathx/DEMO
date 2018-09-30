module("UIMenu", package.seeall)
setmetatable(UIMenu, {__index=UIWidget})

UM_MODEL	= "UM_MODEL"
UM_HERO		= "UM_HERO"
UM_STORE	= "UM_STORE"
UM_EMIAL	= "UM_EMIAL"
UM_EQUIP	= "UM_EQUIP"
UM_TASK		= "UM_TASK"
UM_ARREST	= "UM_ARREST"
UM_EXERCISE	= "UM_EXERCISE"
UM_TRAIN	= "UM_TRAIN"
UM_SCOUNTS	= "UM_SCOUNTS"
UM_PRAISE	= "UM_PRAISE"
UM_AWARDS	= "UM_AWARDS"
UM_INFO		= "UM_INFO"

function new(go)
	local widget = {
	}
	setmetatable(widget, {__index=UIMenu})
	widget:Init(go)
	
	return widget
end

function Init(self, go)
	UIWidget.Init(self, go)
end

function Awake(self)
	self:Install({
		UM_MODEL,
		UM_HERO,
		UM_EMIAL,
		UM_EQUIP,
		UM_TASK,
		UM_ARREST,
		UM_EXERCISE,
		UM_TRAIN,
		UM_SCOUNTS,
		UM_PRAISE,
		UM_AWARDS,
		UM_INFO
	})
	
	self.uiModel = self:AddModel(UM_MODEL)
end

function Start(self)
	local aryPlayMode = {
		UM_TASK, UM_ARREST, UM_EXERCISE, UM_TRAIN, UM_SCOUNTS
	}
	for idx, name in ipairs(aryPlayMode) do
		self:RegisterClickEvent(name, OnMenuClicked)
	end
	
	local aryMove = {
		UM_TASK, UM_ARREST, UM_EXERCISE, UM_TRAIN, UM_SCOUNTS
	}	
	self:DOBackMove(aryMove, true)
end

function Close(self, compelte)
	local group = self.gameObject:GetComponentInChildren(CanvasGroup)
	if group then
		StaticDOTween.DOFade(group, 0, 0.8)
	end
	
	local aryMove = {
		UM_TASK, UM_ARREST, UM_EXERCISE, UM_TRAIN, UM_SCOUNTS
	}
	self:DOBackMove(aryMove, false, compelte)
end

function SetShapeURL(self, url, shapeAssetUrl, complete)
	if not self.uiModel then
		ERROR("Can't find model component")
	else
		self.uiModel:SetURL(url, ObjectLayer.UIMODEL, function(obj)
			obj:SetActive(false)

			if shapeAssetUrl then
				ResourceManager.GetSingleton():LoadAsync(shapeAssetUrl, ShapeAssetObject, function(asset) 
					obj.transform.localPosition 	= asset.LocalPosition
					obj.transform.localScale 		= asset.LocalScale
					obj.transform.eulerAngles 		= asset.LocalAngle
				end)
			end

			local aryMatPath = {
				resmng.Dissolve.Simple, resmng.Dissolve.Reflection
			}
			ResourceManager.GetSingleton():LoadMultiAsync(aryMatPath, Object, function(res)
				obj:SetActive(true)

				ShaderEffect.Dissolve(DissolveType.DT_NORMAL, 
					res.Table, obj, 3, true, function() end)

				if complete then
					complete(obj)
				end
			end)
		end)
	end
end

function OnMenuClicked(self, goSend, evtData)
	local playModeMap = {
		[UM_TASK] 		= resmng.PLAYMODE_TASK,
		[UM_ARREST] 	= resmng.PLAYMODE_ARREST,
		[UM_EXERCISE] 	= resmng.PLAYMODE_EXERCISE,
		[UM_TRAIN] 		= resmng.PLAYMODE_TRAIN,
		[UM_SCOUNTS]	= resmng.PLAYMODE_SCOUNTS
	}
	
	local selectMode = playModeMap[goSend.name]
	if not selectMode then
		ERROR("Can't find play mode define %s", goSend.name)
	else
		local evtArgs = {
		}
		evtArgs.mode	= selectMode
		evtArgs.widget	= self
		
		PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_MENU_MODE, evtArgs)
	end
end






















