using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<Item> _weaponsInChest; // ������ ������ � �������
    public Animator animator;
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���������� �� ������ � ������� � ������ ��� �� ������
        if (collision.CompareTag("Player")&&!isOpen)
        {
            // ��������� ������
            OpenChest();
        }
    }

    public void OpenChest()
    {
        // ��������, ���� �� ������ � �������
        if (_weaponsInChest.Count > 0)
        {
            // �������� ��������� ������ �� ������
            Item randomWeapon = _weaponsInChest[Random.Range(0, _weaponsInChest.Count)];

            // ������� ��������� ���������� ������
            GameObject spawnedWeapon = Instantiate(randomWeapon, transform.position, Quaternion.identity).gameObject;

            // ������������� ��� ����������� ������ ����� ��, ��� � ������ �� ������
            spawnedWeapon.name = randomWeapon.name;

            // ������ ���� "����������"
            isOpen = true;

            // ��������� �������� �������� �������
            animator.SetTrigger("OpenChest");
        }
        else
        {
            Debug.LogWarning("No weapons in the chest!");
        }
    }
}
