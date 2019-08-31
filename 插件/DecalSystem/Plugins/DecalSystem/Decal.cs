//#if UNITY_EDITOR
namespace DecalSystem {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    //using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;


    [ExecuteInEditMode]
    public class Decal : MonoBehaviour {

       

        public Material Material
        {
            get
            {   if (GetComponent<MeshRenderer>() != null)
                {
                    return GetComponent<MeshRenderer>().sharedMaterial;
                }
                
                return null;
            }
            set { }
        }
       [HideInInspector] [FormerlySerializedAs( "sprite" )] public Sprite Sprite;

        [Header("注意:Static物体是不接受贴花的")]
        [FormerlySerializedAs( "affectedLayers" ), FormerlySerializedAs( "AffectedLayers" )] public LayerMask layerMask = 1<<10;
        [HideInInspector] public float MaxAngle = 90.0f;
        [Range(0.005f,0.1f)]
        public float Offset = 0.009f;

        private Vector3 oldScale;

        public MeshRenderer meshRenderer {
            get {
                
                if (gameObject.GetComponent<MeshRenderer>() != null)
                {

                    return gameObject.GetComponent<MeshRenderer>();
                }

                return gameObject.AddComponent<MeshRenderer>();

            }
        }
        public MeshFilter meshFilter
        {
            get
            {

                if (gameObject.GetComponent<MeshFilter>() != null)
                {

                    return gameObject.GetComponent<MeshFilter>();
                }

                return gameObject.AddComponent<MeshFilter>();

            }
        }

        //[Header("高度图除以的值,越低细节越高")]
        //[Range(1f,10f)]
        //public float ResultionDivideValue = 4f;



       


        public void OnValidate() {

            if (!Material) 
            {
                Sprite = null;
            }

            if (Sprite && Material.mainTexture != Sprite.texture)
            {
                Sprite = null;
            }
            //MaxAngle = Mathf.Clamp( MaxAngle, 1, 180 );
            //Offset = Mathf.Clamp( Offset, 0.005f, 0.1f );
            
        }

        void Awake() {



            if (Application.isPlaying == true)
            {

                meshFilter.mesh = new Mesh();
            }
            else
            {
                //var mesh = MeshFilter.sharedMesh;

                //var meshes = GameObject.FindObjectsOfType<Decal>().Select(i => i.MeshFilter.sharedMesh);
                //if (meshes.Contains(mesh)) MeshFilter.sharedMesh = null; // if mesh was copied
                meshFilter.sharedMesh = new Mesh();

            }
           
        }

      

        void OnEnable() {
        }

        void OnWillRenderObject()
        {
            
            if (transform.hasChanged) {
                transform.hasChanged = false;
                BuildAndSetDirty();
            }

          
        }


        void OnDrawGizmosSelected()
        {


            //Debug.Log("OnDrawGizmosSelected");

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube( Vector3.zero, Vector3.one );

            var bounds = DecalUtils.GetBounds( this );
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube( bounds.center, bounds.size + Vector3.one * 0.01f );

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            var mesh = meshFilter.sharedMesh;
            if (mesh)
            {
                var vertices = mesh.vertices;
                var normals = mesh.normals;
                for (var i = 0; i < vertices.Length; i++)
                {
                    Gizmos.DrawRay(vertices[i], normals[i] * 0.15f);
                }
            }
        }


        public void BuildAndSetDirty() {
            //if (Sprite) 
            if (meshRenderer)
            DecalUtils.FixRatio( this, ref oldScale );
            DecalBuilder.Build( this );
            //DecalUtils.SetDirty( this );
        }


    }
}
//#endif