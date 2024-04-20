using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;


namespace GorillaZilla
{
    /// <summary>
    ///     Attach this component to your building to enable destructable behavior
    /// </summary>
    public class DestructableBuilding : MonoBehaviour
    {
        [SerializeField] GameObject hitParticlePrefab;
        [SerializeField] Transform buildingPartsRoot;
        [SerializeField] Transform fullModel;
        [SerializeField] float minDestructionForce = 0f;

        private bool isCrumbled = false;
        private List<BuildingPiece> buildingPieces = new List<BuildingPiece>();

        public UnityEvent onBuildingHit;
        public bool isDestructable = true;

        void Start()
        {
            SetupBuilding();
        }

        //Setup and hide pieces to appear as solid object
        private void SetupBuilding()
        {
            //Add BuildingPiece component to each mesh renderer in body. 
            //Hide pieces and enable full model
            var rnds = buildingPartsRoot.GetComponentsInChildren<MeshRenderer>();
            foreach (var rnd in rnds)
            {
                SetupPiece(rnd.gameObject);
                rnd.enabled = false;
            }
            fullModel.gameObject.SetActive(true);
        }
        private void SetupPiece(GameObject pieceGO)
        {
            var piece = pieceGO.AddComponent<BuildingPiece>();
            buildingPieces.Add(piece);
        }

        //Enables physics on the appropriate pieces
        private void Crumble(Vector3 hitPosition, Vector3 hitVelocity)
        {
            if (hitVelocity.magnitude > minDestructionForce)
            {
                if (!isCrumbled)
                {
                    //Re-enable visuals of pieces
                    fullModel.gameObject.SetActive(false);
                    var rnds = buildingPartsRoot.GetComponentsInChildren<MeshRenderer>();
                    foreach (var rnd in rnds)
                    {
                        rnd.enabled = true;
                    }
                    onBuildingHit?.Invoke();
                    isCrumbled = true;
                }

                //Create explosion effect
                var particle = Instantiate(hitParticlePrefab, hitPosition, transform.rotation);
                particle.transform.localScale = Vector3.one * .05f;

                //Apply physics force to all pieces above hit position

                for (int i = 0; i < buildingPieces.Count; i++)
                {
                    var piece = buildingPieces[i];
                    if (piece.GetMaxHeight() >= hitPosition.y)
                    {
                        piece.CrumblePiece(hitVelocity);
                        buildingPieces.Remove(piece);
                    }
                }
            }
        }
        public void OnPieceCollision(BuildingPiece hitPiece, Collision collision)
        {
            if (!isDestructable) return;
            hitPiece.CrumblePiece(collision.relativeVelocity);
            buildingPieces.Remove(hitPiece);
            Vector3 hitDetectPoint = collision.GetContact(0).point;
            hitDetectPoint.y = hitPiece.GetMinHeight();
            Crumble(hitDetectPoint, collision.relativeVelocity);
        }
    }

}
