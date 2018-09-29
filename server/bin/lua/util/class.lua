function mkcall( module )
    local mt = {}
    mt.__index = _G
    mt.__call = function(func, ...) return func.new(...) end
    setmetatable( module, mt )
end

function class( module )
    module.mt = module.mt or {__index=module}
    module.new = function(...)
        local ins = {}
        setmetatable(ins, module.mt)
        if ins.Awake then ins:Awake(...) end
        return ins
    end

    local mt = {}
    mt.__index = _G
    mt.__call = function(func, ...) return func.new(...) end
    setmetatable( module, mt )
end

function singleton( module )
	module.mt = module.mt or {__index=module}
	module.new = function()
		local ins = {}
		setmetatable(ins, module.mt)
		if ins.init then ins:init() end
		return ins
	end
	module.getInstance = function()
		local ins = rawget(module, "instance_")
		if not ins then
			ins = module.new()
			rawset(module, "instance_", ins)
		end
		return ins
	end

	local mt = {}
	mt.__index = _G
	mt.__call = function(func)
		return func.getInstance()
	end
	setmetatable( module, mt )
end

function extendsClass( child, parent )
	child.mt = child.mt or {__index=child}
	child.new = function(...)
		local ins = {}
		setmetatable(ins, child.mt)
		
		if ins.Awake then
			if parent and parent.Awake then
				parent.Awake(ins, ...)
			end

			ins:Awake(...) 
		end
		
		return ins
	end

	local mt = {}
	mt.__index = parent or _G
	mt.__call = function(func, ...) return func.new(...) end
	setmetatable( child, mt )
end

function singletonClass( child, parent )
	child.mt = child.mt or {__index=child}
	child.new = function()
		local ins = {}
		setmetatable(ins, child.mt)
		if ins.init then ins:init() end
		return ins
	end
	child.getInstance = function()
		local ins = rawget(child, "instance_")
		if not ins then
			ins = child.new()
			rawset(child, "instance_", ins)
		end
		return ins
	end

	local mt = {}
	mt.__index = parent or _G
	mt.__call = function(func)
		return func.getInstance()
	end
	setmetatable( child, mt )
end


