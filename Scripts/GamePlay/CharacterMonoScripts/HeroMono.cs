﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 英雄单位的Mono类
/// </summary>
public class HeroMono : CharacterMono{
    public HPViewModel HPViewModel;
    public AvatarViewModel avatarViewModel;

    public HeroModel HeroModel {
        get {
            return characterModel as HeroModel;
        }
    }

    public override bool Attack(ref bool isAttackFinish, Transform targetTransform, CharacterMono target) {
        bool result = base.Attack(ref isAttackFinish, targetTransform, target);
        if (target.isDying || target == null) {
            HeroModel.Exp += target.characterModel.supportExp;
        }
        return result;
    }

    /// <summary>
    /// 单位获得物品的方法，返回True表示单位成功获得该物品，
    /// 返回Fals表示因为单位物品栏限制，单位获得物品失败。
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool GetItem(ItemGrid item) {
        for (int i=0;i<HeroModel.itemGrids.Count;i++) {
            ItemGrid itemGrid = HeroModel.itemGrids[i];
            if (itemGrid.item == null) {
                itemGrid.item = item.item;
                itemGrid.ItemCount = item.ItemCount;
                return true;
            }
        }
        return false;
    }

    public override void Update() {
        base.Update();
    }

    #region UI和Model之间的绑定
    protected override void Bind() {
        base.Bind();
        characterModel.HpValueChangedHandler += OnHpChanged;
        HeroModel.ExpChangedHandler += OnExpChanged;
        HeroModel.LevelChangedHandler += OnLevelChanged;
    }
    public void OnHpChanged(int oldHp, int newHp) {
        HPViewModel.Hp.Value = newHp;
    }
    public void OnExpChanged(int oldExp,int newExp) {

        //==========================================================================
        // 修改UI
        int expRate = Mathf.Clamp(Mathf.FloorToInt( ( (float)newExp / HeroModel.NextLevelNeedExp ) * 100 ),0,100);
        avatarViewModel.ExpRate.Value = expRate;

        //==============================
        // 当经验值满值时,修改等级
        if (expRate == 100) {
            HeroModel.Level += 1;
        }
    }
    public void OnLevelChanged(int oldLevel, int newLevel) {
        if (newLevel > oldLevel) {
            HeroModel.SkillPoint += HeroModel.skillPointGrowthPoint * (newLevel - oldLevel);
        }
        HeroModel.Exp = 0;
        avatarViewModel.Level.Value = newLevel;
    }
    #endregion
}

