using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedido {

    private string name;

    public Pedido (string name) {
        this.name = name;
    }

	public string getName() {
		return this.name;
	}

	public void setName(string name) {
		this.name = name;
	}

}
