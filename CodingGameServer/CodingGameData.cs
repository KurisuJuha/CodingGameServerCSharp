using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

namespace CodingGameBase;


public abstract class CodingGameData
{
    public string Name { get; private set; }
    public byte[] data;

    public byte[] ToBytes()
    {
        DataWriter writer = new DataWriter();
        writer.Put(Name);
        writer.Put(data);

        return writer.GetData();
    }
}
