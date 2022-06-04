using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deviloper.Character;
using Deviloper.Core;

namespace Deviloper.Service.Character
{
    public class CharacterService : MonoSingletonGeneric<CharacterService>
    {
        private PlayerController player;
        private List<EnemyController> enemies;
    }
}
