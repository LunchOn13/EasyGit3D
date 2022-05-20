using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 작성자 : 곽진성,
    /// 기능 : 변경사항 모델 정보
    /// </summary>
    public class StatusModel : MonoBehaviour
    {
        private BranchModel belongBranch;
        private MeshRenderer meshRenderer;
        
        private Material baseMaterial;
        private Material highlightMaterial;

        private string path;

        [SerializeField] Material addedMaterial;
        [SerializeField] Material modifiedMaterial;
        [SerializeField] Material deletedMaterial;
        [SerializeField] Material untrackedMaterial;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetBelongBranch(BranchModel branch)
        {
            belongBranch = branch;
        }

        public void SetInformation(string _path)
        {
            path = _path;
        }

        public void ApplyMaterial(StatusCategory category)
        {
            // 변경사항의 종류에 따라 다른 매터리얼 적용
            switch (category)
            {
                case StatusCategory.Added:
                    meshRenderer.material = addedMaterial;
                    baseMaterial = addedMaterial;
                    break;

                case StatusCategory.Modified:
                    meshRenderer.material = modifiedMaterial;
                    baseMaterial = modifiedMaterial;
                    break;

                case StatusCategory.Deleted:
                    meshRenderer.material = deletedMaterial;
                    baseMaterial = deletedMaterial;
                    break;

                case StatusCategory.Untracked:
                    meshRenderer.material = untrackedMaterial;
                    baseMaterial = untrackedMaterial;
                    break;

                default:
                    break;
            }
        }
        private void OnMouseEnter()
        {
            //meshRenderer.material = highlightMaterial;
            Message.Instance.SetMessage(path, Input.mousePosition);
        }

        private void OnMouseExit()
        {
            //meshRenderer.material = baseMaterial;
            Message.Instance.SetMessage("", transform.position);
        }
    }
}