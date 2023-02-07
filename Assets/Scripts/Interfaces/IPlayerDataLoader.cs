using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public interface IPlayerDataLoader
    {
        PlayerData PlayerData { get; set; }
    }
}