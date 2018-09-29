module("Object", class)

function Awake(self, uid)
    self.uid        = uid or uvcore.hash_string(tostring(os.time() + math.random() * math.random()))
    self.typeHash   = uvcore.hash_string(self:GetTypeName())
end

function GetID(self)
    return self.uid
end

function SetID(self, uid)
    self.uid = uid
end

function GetTypeName(self)
    return self._NAME
end

function GetTypeHash(self)
    return self.typeHash
end