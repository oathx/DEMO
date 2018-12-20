using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

[CustomLuaClass]
public class XActorComponent : MonoBehaviour
{
    [CustomLuaClass]
    public enum BoneNogType
    {
        None, 
        RHand, 
        LHand, 
        Hit, 
        RFoot, 
        LFoot, 
        Center, 
        Head, 
        Pelvis,
        RElbow, 
        LElbow, 
        RKnee, 
        LKnee, 
        RWeapon, 
        LWeapon, 
        REye, 
        LEye,
        Reserve1, 
        Reserve2, 
        Reserve3, 
        LshoulderArmor, 
        RshoulderArmor,
        LUpperArm, 
        RUpperArm, 
        LForearm, 
        RForearm, 
        Spine, 
        Spine1, 
        Spine2,
        LThigh, 
        RThigh, 
        LCalf, 
        RCalf
    };

    [System.Serializable, CustomLuaClass]
    public class BoneNogPoint
    {
        public Transform head = null;
        public Transform hit = null;
        public Transform pelvis = null;
        public Transform center = null;
        public Transform leftElbow = null;
        public Transform rightElbow = null;
        public Transform leftHand = null;
        public Transform rightHand = null;
        public Transform leftKnee = null;
        public Transform rightKnee = null;
        public Transform leftFoot = null;
        public Transform rightFoot = null;
        public Transform leftWeapon = null;
        public Transform rightWeapon = null;
        public Transform leftEye = null;
        public Transform rightEye = null;
        public Transform reserve1 = null;
        public Transform reserve2 = null;
        public Transform reserve3 = null;

        public Transform leftshoulderArmor = null;
        public Transform rightshoulderArmor = null;
        public Transform leftUpperArm = null;
        public Transform rightUpperArm = null;
        public Transform leftForearm = null;
        public Transform rightForearm = null;
        public Transform spine = null;
        public Transform spine1 = null;
        public Transform spine2 = null;
        public Transform leftThigh = null;
        public Transform rightThigh = null;
        public Transform leftCalf = null;
        public Transform rightCalf = null;
    }

    [HideInInspector, System.NonSerialized]
    public BoneNogPoint nogPoint;

    [HideInInspector, System.NonSerialized]
    public GameObject   checkPoint;

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, AnimationClip> 
        overridedAnimation = new Dictionary<string, AnimationClip>();
    
    /// <summary>
    /// 
    /// </summary>
    public Animator animator;

    /// <summary>
    /// 
    /// </summary>
    [ContextMenu("Generate Bone NogPoint")]
    public void generateBoneNogPoint()
    {
        nogPoint = new BoneNogPoint();
        BoneTransformToNog();
        findAllNogPoint(transform);
    }

    /// <summary>
    /// 
    /// </summary>
    void BoneTransformToNog()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        nogPoint.head       = animator.GetBoneTransform(HumanBodyBones.Head);
        nogPoint.hit        = animator.GetBoneTransform(HumanBodyBones.Chest);
        nogPoint.pelvis     = animator.GetBoneTransform(HumanBodyBones.Hips);
        nogPoint.leftElbow  = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        nogPoint.leftHand   = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        nogPoint.rightElbow = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
        nogPoint.rightHand  = animator.GetBoneTransform(HumanBodyBones.RightHand);
        nogPoint.leftKnee   = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        nogPoint.rightKnee  = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        nogPoint.leftFoot   = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        nogPoint.rightFoot  = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        nogPoint.leftEye    = animator.GetBoneTransform(HumanBodyBones.LeftEye);
        nogPoint.rightEye   = animator.GetBoneTransform(HumanBodyBones.RightEye);

        nogPoint.center = transform.Find("shape");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="root"></param>
    private void    findAllNogPoint(Transform root)
    {
        foreach (Transform child in root)
        {
            string name = child.name.ToLower();
            if (name == "head_empty")
                nogPoint.head = child;
            else if (name == "hit_empty") 
                nogPoint.hit = child;
            else if (name == "pelvis_empty")
                nogPoint.pelvis = child;
            else if (name == "center_empty")
                nogPoint.center = child;
            else if (name == "l_anc_empty")
                nogPoint.leftElbow = child;
            else if (name == "l_h_empty")
                nogPoint.leftHand = child;
            else if (name == "r_anc_empty")
                nogPoint.rightElbow = child;
            else if (name == "r_h_empty")
                nogPoint.rightHand = child;
            else if (name == "l_knee_empty")
                nogPoint.leftKnee = child;
            else if (name == "r_knee_empty")
                nogPoint.rightKnee = child;
            else if (name == "l_f_empty")
                nogPoint.leftFoot = child;
            else if (name == "r_f_empty")
                nogPoint.rightFoot = child;
            else if (name == "l_weapon")
                nogPoint.leftWeapon = child;
            else if (name == "r_weapon")
                nogPoint.rightWeapon = child;
            else if (name == "bip01")
                nogPoint.center = child;

            findAllNogPoint(child);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bindType"></param>
    /// <returns></returns>
    public Transform        GetBoneNogPoint(BoneNogType bindType)
    {
        Transform nogPointTrans = null;

        switch (bindType)
        {
            case BoneNogType.Head:
                nogPointTrans = nogPoint.head;
                break;
            case BoneNogType.Hit:
                nogPointTrans = nogPoint.hit;
                break;
            case BoneNogType.Pelvis:
                nogPointTrans = nogPoint.pelvis;
                break;
            case BoneNogType.Center:
                nogPointTrans = nogPoint.center;
                break;
            case BoneNogType.LElbow:
                nogPointTrans = nogPoint.leftElbow;
                break;
            case BoneNogType.RElbow:
                nogPointTrans = nogPoint.rightElbow;
                break;
            case BoneNogType.LHand:
                nogPointTrans = nogPoint.leftHand;
                break;
            case BoneNogType.RHand:
                nogPointTrans = nogPoint.rightHand;
                break;
            case BoneNogType.LKnee:
                nogPointTrans = nogPoint.leftKnee;
                break;
            case BoneNogType.RKnee:
                nogPointTrans = nogPoint.rightKnee;
                break;
            case BoneNogType.LFoot:
                nogPointTrans = nogPoint.leftFoot;
                break;
            case BoneNogType.RFoot:
                nogPointTrans = nogPoint.rightFoot;
                break;
            case BoneNogType.LWeapon:
                nogPointTrans = nogPoint.leftWeapon;
                break;
            case BoneNogType.RWeapon:
                nogPointTrans = nogPoint.rightWeapon;
                break;
            case BoneNogType.Reserve1:
                nogPointTrans = nogPoint.reserve1;
                break;
            case BoneNogType.Reserve2:
                nogPointTrans = nogPoint.reserve2;
                break;
            case BoneNogType.Reserve3:
                nogPointTrans = nogPoint.reserve3;
                break;
            case BoneNogType.LshoulderArmor:
                nogPointTrans = nogPoint.leftshoulderArmor;
                break;
            case BoneNogType.RshoulderArmor:
                nogPointTrans = nogPoint.rightshoulderArmor;
                break;
            case BoneNogType.LUpperArm:
                nogPointTrans = nogPoint.leftUpperArm;
                break;
            case BoneNogType.RUpperArm:
                nogPointTrans = nogPoint.rightUpperArm;
                break;
            case BoneNogType.LForearm:
                nogPointTrans = nogPoint.leftForearm;
                break;
            case BoneNogType.RForearm:
                nogPointTrans = nogPoint.rightForearm;
                break;
            case BoneNogType.Spine:
                nogPointTrans = nogPoint.spine;
                break;
            case BoneNogType.Spine1:
                nogPointTrans = nogPoint.spine1;
                break;
            case BoneNogType.Spine2:
                nogPointTrans = nogPoint.spine2;
                break;
            case BoneNogType.LCalf:
                nogPointTrans = nogPoint.leftCalf;
                break;
            case BoneNogType.RCalf:
                nogPointTrans = nogPoint.rightCalf;
                break;
        }

        if (nogPointTrans == null)
            nogPointTrans = transform;

        return nogPointTrans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Animator         GetAnimator()
    {
        return animator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clip"></param>
    public virtual void     SetAnimatorOverrideAnimation(string name, AnimationClip clip)
    {
        if (!animator) 
            return;

        AnimatorOverrideController controller = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (controller != null && clip != null)
        {
            AnimationClip old = controller[name];
            controller[name] = clip;

            if (!overridedAnimation.ContainsKey(name))
                overridedAnimation.Add(name, old);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clip"></param>
    public virtual void     SetAnimatorOverrideAnimation(string name, string clip)
    {
        if (!animator) 
            return;

        AnimatorOverrideController controller = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (controller != null && !string.IsNullOrEmpty(clip))
        {
            AnimationClip old = controller[name];
            controller[name] = controller[clip];

            if (!overridedAnimation.ContainsKey(name))
                overridedAnimation.Add(name, old);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void             RevertOverrideAnimation(string name)
    {
        if (!animator) 
            return;

        AnimationClip clip = null;
        overridedAnimation.TryGetValue(name, out clip);

        if (clip != null)
        {
            AnimatorOverrideController controller = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (controller != null && clip != null)
            {
                controller[name] = clip;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual AnimatorStateInfo GetCurrentAnimatorState()
    {
        if (animator)
        {
            if (animator.IsInTransition(0))
                return animator.GetNextAnimatorStateInfo(0);

            return animator.GetCurrentAnimatorStateInfo(0);
        }

        return default(AnimatorStateInfo);
    }
}

