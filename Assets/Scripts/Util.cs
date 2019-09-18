namespace Utilities {
    using UnityEngine;

    public static class Util {
        public static void SmoothLookAtPos(this Transform transf, Vector3 pointedWorldPos, float turnSpeed, float t, float dt)
        {
            var direction = pointedWorldPos - transf.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            var targetRotation = Quaternion.Euler(0f, 0f, angle);

            var smoothRotation = Quaternion.Slerp(transf.rotation, targetRotation, t);

            transf.rotation = Quaternion.RotateTowards(transf.rotation, smoothRotation, turnSpeed * dt);
        }  
    }
}