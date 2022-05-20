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
        private AddPanel addPanel;

        private string path;

        [SerializeField] Material addedMaterial;
        [SerializeField] Material modifiedMaterial;
        [SerializeField] Material deletedMaterial;
        [SerializeField] Material untrackedMaterial;

        [SerializeField] Material addedHighlightMaterial;
        [SerializeField] Material modifiedHighlightMaterial;
        [SerializeField] Material deletedHighlightMaterial;
        [SerializeField] Material untrackedHighlightMaterial;

        public void Initialize()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            addPanel = GameObject.Find("Add Panel").GetComponent<AddPanel>();
        }

        public void SetBelongBranch(BranchModel branch)
        {
            belongBranch = branch;
        }

        public void SetInformation(string _path)
        {
            path = _path;
            addPanel.AddStatusModel(this);
        }

        public string GetPath()
        {
            return path;
        }

        public void ApplyMaterial(StatusCategory category)
        {
            // 변경사항의 종류에 따라 다른 매터리얼 적용
            switch (category)
            {
                case StatusCategory.Added:
                    meshRenderer.material = addedMaterial;
                    baseMaterial = addedMaterial;
                    highlightMaterial = addedHighlightMaterial;
                    break;

                case StatusCategory.Modified:
                    meshRenderer.material = modifiedMaterial;
                    baseMaterial = modifiedMaterial;
                    highlightMaterial = modifiedHighlightMaterial;
                    break;

                case StatusCategory.Deleted:
                    meshRenderer.material = deletedMaterial;
                    baseMaterial = deletedMaterial;
                    highlightMaterial = deletedHighlightMaterial;
                    break;

                case StatusCategory.Untracked:
                    meshRenderer.material = untrackedMaterial;
                    baseMaterial = untrackedMaterial;
                    highlightMaterial = untrackedHighlightMaterial;
                    break;

                default:
                    break;
            }
        }

        public void HighlightModel()
        {
            meshRenderer.material = highlightMaterial;
        }

        public void ReturnBaseModel()
        {
            meshRenderer.material = baseMaterial;
        }

        private void OnMouseEnter()
        {
            Message.Instance.SetMessage(path, Input.mousePosition);
        }

        private void OnMouseDown()
        {
            if(!StageManager.CheckStatusModel(path))
                addPanel.ShowPanel(Input.mousePosition, path);
        }

        private void OnMouseExit()
        {
            Message.Instance.SetMessage("", transform.position);
        }
    }
}