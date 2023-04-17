using System;
using ECS.ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Unit
{
	[SerializeField] private int health;
	[SerializeField] private float attackDamage;
	[SerializeField] private float attackRange;
	[SerializeField] private float attackCooldown;
	[SerializeField] private float movementSpeed;
	[SerializeField] private Team team;

	public int Health => health;
	public float AttackDamage => attackDamage;
	public float AttackRange => attackRange;
	public float AttackCooldown => attackCooldown;
	public float MovementSpeed => movementSpeed;
	public Team Team => team;

	public float3 Position { get; set; }
	public Entity DisplayEntity { get; set; }

	public void TestInit(Team team)
	{
		health = 100;
		attackDamage = 10;
		attackRange = 2;
		attackCooldown = 1;
		movementSpeed = 4;
		this.team = team;
	}
}