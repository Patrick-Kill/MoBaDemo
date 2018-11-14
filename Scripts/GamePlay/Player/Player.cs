﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 玩家类，用来区分不同的玩家，同时管理各个单位的控制权，
/// </summary>
public class Player {

    /// <summary>
    /// 玩家所持金钱
    /// </summary>
    private int money;

    public int Money {
        get {
            return money;
        }

        set {
            money = value;
        }
    }
}

