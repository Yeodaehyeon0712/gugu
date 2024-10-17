using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StatComponent : BaseComponent
{
    #region Fields
    Data.CharacterData characterData;
    float moveSpeed;
    float maxHP;
    #endregion

    #region Component Method
    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent,useUpdate:false)
    {

    }
    #endregion
    public void SetStat(Data.CharacterData characterData)
    {
        this.characterData = characterData;
        moveSpeed = characterData.MoveSpeed;
        maxHP = characterData.HP;
    }
    //���� ���� , ���������� ���� ��ġ�� �������� ���� .
    //����
    //��ų�� ������ ���� ..

    //�켭����ũ�� �ɷ�ġ
    //
}
