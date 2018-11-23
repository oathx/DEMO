module("UISystem", mkSingleton)
setmetatable(UISystem, {__index=LuaBehaviour})

function Init(self)
	self.widgets 	= {}
	self.cacheQueue = {}
end

function Startup(self)
	self.gameObject = GameObject("UI")
	self:AddLuaBehaviour(self.gameObject)

	return true
end

function Awake(self)
	self.uiCamera 		= GameObject("UICamera")
	self.canvasObject	= GameObject("Canvas")
	self.eventSystem	= GameObject("EventSystem")
end

function Start(self)
	self.gameObject.layer = LayerMask.NameToLayer("UI")
	GameObject.DontDestroyOnLoad(self.gameObject)
	
	local aryTrans = {
		self.uiCamera, self.canvasObject, self.eventSystem
	}
	
	for idx, go in ipairs(aryTrans) do
		go.layer 			= LayerMask.NameToLayer("UI")
		go.transform.parent = self.transform
	end
	
	self.eventSystem:AddComponent(EventSystem)
	if _EDITOR then
		self.eventSystem:AddComponent(StandaloneInputModule)
	else
		self.eventSystem:AddComponent(TouchInputModule)
	end
	
	self.canvas = self.canvasObject:AddComponent(Canvas)
	if self.canvas then
		self.uguiCamera 			= self.uiCamera:AddComponent(Camera)
		self.uguiCamera.cullingMask = bit.lshift(1, self.gameObject.layer)
		self.uguiCamera.depth 		= 65535
		self.uguiCamera.clearFlags 	= CameraClearFlags.Depth
		
		self.canvas.worldCamera		= self.uguiCamera
		self.canvas.renderMode 		= RenderMode.ScreenSpaceCamera	
		
		self.rectTransfrom 			= self.canvasObject:GetComponent(RectTransform)
		self.screenRect 			= self.rectTransfrom.rect;		
	end
	
	self.raycaster 		= self.canvasObject:AddComponent(GraphicRaycaster)
	self.canvasScaler	= self.canvasObject:AddComponent(CanvasScaler)
	if self.canvasScaler then
		self.canvasScaler.uiScaleMode = ScaleMode.ScaleAndCrop
		self.canvasScaler.referenceResolution = Vector2(800, 600)
	end
end

function WorldToUIPoint(self, pos)
	if not Camera.main then
		return self.uguiCamera:WorldToScreenPoint(pos)
	end

	local wpos = Camera.main:WorldToScreenPoint(pos)
	return self.uguiCamera:ScreenToWorldPoint(wpos)	
end

function LoadAsyncWidget(self, szResource, szModule, complete, cache)
	local name = GetFileName(szModule)
	if not name then
		ERROR("Widget lua class name error %s", szModule)
	else
		local widget = self.widgets[name]
		if not widget then
			XResourceManager.GetSingleton():LoadAsync(szResource, GameObject, function(obj) 
				widget = self:CreateWidget(name, szResource, szModule, obj, cache)
				
				if complete then
					complete(widget)
				end
			end)
		end
	end
end

function CreateWidget(self, name, szResource, szModule, goResource, cache)
	local goWidget = GameObject.Instantiate(goResource)
	if not goWidget then
		ERROR("Can't instantiate GameObject(%s)", goResource)
	else
		goWidget.transform.position = Vector3.zero
		goWidget.transform:SetParent(self.canvasObject.transform, false)
		goWidget.name				= name
		
		local rt = goWidget:GetComponent(RectTransform)
		if rt then
			rt.offsetMax 	= Vector2.zero
			rt.offsetMin 	= Vector2.zero
			rt.localScale	= Vector3.one
			rt.localPosition= Vector3.zero
		end
		
		local mod = require(szModule)
		if mod then
			local widget = _G[name].new(goWidget)
			if not widget then
				ERROR("Can't construct widget %s", szModule)
			end
			
			if cache then
				self.widgets[name] = widget
			end
			
			INFO("Create widget name(%s) resource(%s), module(%s) cache(%s)",
				name, szResource, szModule, cache)
				
			return widget
		end		
	end
end

function LoadWidget(self, szResource, szModule, cache)
	local name = GetFileName(szModule)
	if not name then
		ERROR("Widget lua class name error %s", szModule)
	else
		local widget = self.widgets[name]
		if not widget then
			local goResource = Resources.Load(szResource)
			if not goResource then
				LOG_ERROR("Can't load widget resource %s class(%s)", szResource, szModule)
			else
				return self:CreateWidget(name, szResource, szModule, goResource, cache)
			end
		end
	end
end

function GetWidget(self, szModule)
    local name = GetFileName(szModule)
    if not name then
        ERROR("module name is error %s", szModule)
    end

    return self.widgets[name]
end

function UnloadWidget(self, szModule, complete)
	local name = GetFileName(szModule)
	if not name then
		ERROR("module name is error %s", szModule)
	end

	local widget = self.widgets[name]
	if widget then
		INFO("Unload widget name(%s) module(%s)", name, szModule)

		widget:Close(function()			
			local setting = widget:GetConfigure()
			local evtArgs = {
				style = setting.ID, widget = widget
			}

			PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_CLOSED, evtArgs)

			if setting.cache then
				table.insert(self.cacheQueue, setting.ID)
			end

			if complete then
				complete()
			end
			
			GameObject.Destroy(
				widget.gameObject
				)
				
			self.widgets[name] = nil
		end)
	else
		if complete then complete() end
	end
end

function Clearup(self)
	for name, widget in pairs(self.widgets) do
		GameObject.Destroy(widget.gameObject)
	end
	
	self.widgets = {}
end

function Shutdown(self)
	self:Clearup()
end

function OpenWidget(self, style, complete)
	local setting = UIConfigure[style]
	if not setting then
		ERROR("Can't find ui config(%s)", tostring(style))
	else
		local opened = function(widget)
			if widget then
				if not setting.active then
					widget:Hide()
				end

				widget:Configure(setting)

				local evtArgs = {
					widget = widget,
				}
				PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_OPENED, evtArgs)

				if complete then
					complete(widget)
				end
			end
		end

		if setting.async then
			self:LoadAsyncWidget(setting.prefab, setting.uiClass, function(widget) 
				opened(widget)
			end, setting.cache)
		else
			opened(self:LoadWidget(setting.prefab,
				setting.uiClass, setting.cache))
		end
	end
end

function CloseWidget(self, style, complete)
    local set = UIConfigure[style]
    if set then
    	self:UnloadWidget(set.uiClass, complete)    
    end
end

function MessageBox(self, style, text, callback, args)
	self:OpenWidget(style, function(widget) 
		if widget then
			widget:Message(text, callback, args)
		end
	end)
end