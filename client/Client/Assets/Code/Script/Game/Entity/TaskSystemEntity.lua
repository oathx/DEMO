module("TaskSystemEntity", package.seeall)
setmetatable(TaskSystemEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}
	setmetatable(entity, {__index=TaskSystemEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)

	self.tasks_ 	= {}
	self.current_ 	= self:Create(resmng.TASK_TEST)
end

function Create(self, id)
	local task = self:Query(id)
	if task then
		ELOG("Can't add task id(%s)", tostring(id))
	else
		task = TaskEntity.new(id)
		table.insert(self.tasks_, 
			task)
	end

	return task
end

function Query(self, id)
	for idx, task in ipairs(self.tasks_) do
		if task:ID() == id then
			return task
		end
	end
end

function GetCurrent(self)
	return self.current_
end

