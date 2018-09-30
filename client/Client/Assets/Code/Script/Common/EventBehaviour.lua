module("EventBehaviour", package.seeall)
setmetatable(EventBehaviour, {__index=LuaBehaviour})

function new(szName)
	local obj = {
	}
	setmetatable(obj, {__index=EventBehaviour})
	obj:Init(szName)
	
	return obj
end

function Init(self, szName)
	self.gameObject = GameObject(szName)
	self:AddLuaBehaviour(self.gameObject)
	
	self.events = {
	}
end

function GetName(self)
	return self.gameObject.name
end

function SetDispatcher(self, dispatcher)
	self.dispatcher = dispatcher
end

function GetDispatcher(self)
	return self.dispatcher
end

function SubscribeEvent(self, nID, evtCallback)
	if not self.events[nID] then
		self.events[nID] = evtCallback
	end
end

function FireEvent(self, nID, evtArgs)
	return self.events[nID](self, evtArgs)
end

function HasEvent(self, nID)
	return self.events[nID]
end

function RemoveEvent(self, nID)
	if self.events[nID] then
		self.events[nID] = nil
	end
end

function RemoveAllEvent(self)
	self.events = {}
end



