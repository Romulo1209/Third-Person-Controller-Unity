using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversedKinematic : MonoBehaviour
{
    #region Getters

    public Vector3 RightFeetPosition { 
        get { 
            var pos = rightFootPosition; 
            pos.y += 0.3f; 
            return pos; 
        }
    }
    public Vector3 LeftFeetPosition { 
        get {
            var pos = leftFootPosition;
            pos.y += 0.3f;
            return pos; 
        }
    }

    #endregion

    private Vector3 rightFootPosition, leftFootPosition, rightFootIKPosition, leftFootIKPosition;
    private Quaternion leftFootIKRotation, rightFootIKRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet Grounder")]
    [SerializeField] Animator anim;
    [Space(20)]

    public bool enableFeetIK = true;
    [Range(0, 2)][SerializeField] float heightFromGroundCast = 1.14f;
    [Range(0, 2)] [SerializeField] float raycastDownDistance = 1.5f;
    [SerializeField] LayerMask enviromentLayer;
    [SerializeField] float pelvisOffset = 0;
    [Range(0, 1)] [SerializeField] float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] float feetToIKPositionSpeed = 0.5f;

    [SerializeField] string leftFootAnimVariableName = "LeftFootCurve";
    [SerializeField] string rightFootAnimVariableName = "RightFootCurve";

    public bool UseProIKFeature = false;
    public bool ShowSolverDebug = true;

    private void FixedUpdate()
    {
        if (anim == null) return;

        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        if (!enableFeetIK) return;

        //Find and raycast to the ground to find positions
        FeetPositionSolver(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation);
        FeetPositionSolver(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!enableFeetIK) return;
        if (anim == null) return;

        MovePelvisHeight();

        //right foot ik and rotation -- pro features here
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        if (UseProIKFeature) {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }
        MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);

        //left foot ik and rotation -- pro features here
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        if (UseProIKFeature) {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }
        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
    }

    #region Feet Grounding Methods

    private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
    {
        Vector3 targetIKPosition = anim.GetIKPosition(foot);
        if(positionIKHolder != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);
            targetIKPosition.y += yVariable;

            lastFootPositionY = yVariable;

            targetIKPosition = transform.TransformPoint(targetIKPosition);

            anim.SetIKRotation(foot, rotationIKHolder);
        }

        anim.SetIKPosition(foot, targetIKPosition);
    }

    private void MovePelvisHeight()
    {
        if(rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastPelvisPositionY == 0) {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }
        float lOffsetPosition = leftFootIKPosition.y - transform.position.y;
        float rOffsetPosition = rightFootIKPosition.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;
        Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;
        newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);
        anim.bodyPosition = newPelvisPosition;
        lastPelvisPositionY = anim.bodyPosition.y;
    }

    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPositions, ref Quaternion feetIKRotations)
    {
        //raycast handling section
        RaycastHit feetOutHit;

        if (ShowSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundCast), Color.yellow);

        if(Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundCast, enviromentLayer))
        {
            //finding out feet ik positions from the sky
            feetIKPositions = fromSkyPosition;
            feetIKPositions.y = feetOutHit.point.y + pelvisOffset;
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
            return;
        }

        feetIKPositions = Vector3.zero; //dont work
    }

    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundCast;
    }

    #endregion
}
