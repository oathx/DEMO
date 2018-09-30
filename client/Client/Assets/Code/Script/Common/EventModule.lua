module("EventModule", package.seeall)
setmetatable(EventModule, {__index=BaseEntity})

function new(szName)
	local obj = {
	}
	setmetatable(obj, {__index=EventModule})
	obj:Init(szName)
	
	return obj
end

function Init(self, szName)
	BaseEntity.Init(self, 0)

	self.events = {
	}
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



