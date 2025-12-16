using System;

namespace UI.DTOs;

public class OrderedItems<T, ItemType>
{
    public T Key { get; set; } = default!;

    public List<ItemType> Items { get; set; } = default!;
}
