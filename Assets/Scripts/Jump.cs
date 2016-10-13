using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpForce = 3f;
	public GameObject alien;
	public GameObject floor;

	//Função executada para inicialização
	void Start () {
	
		//Instancia o objeto player criado na Scena para ser manipulado pelo script
		alien = GameObject.FindGameObjectWithTag("Alien");
		floor = GameObject.Find ("Floor");
	
	}
	
	// Update executada uma vez a cada quadro
	void Update () {

		//Verifica se o usuário tocou o botão, no aparelho do jogador
		if(Application.platform == RuntimePlatform.Android){

			/* Esses testes servem para avaliar o estado do botão ao ser tocado pelo jogador.
			 * 
			 */
			if (Input.touchCount > 0) //Se houver toque
			{
				/* O TouchPhase é um objeto que contém os estados do toque na tela */
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					/* Quando pressionado
					 * O estado Began, permanece enquanto não soltar
					 */
					Debug.Log ("Tocou a tela");
					CheckTouch (Input.GetTouch(0).position, "began");
				}
				else if(Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					/* Quando tirar o dedo da tela
					 * O estado Ended ocorre quando o botão é solto
					 */
					Debug.Log ("Tirou o dedo da tela");
					CheckTouch (Input.GetTouch(0).position, "ended");
				}
			}
		}

		/* Verifica se o usuário está tocando o botão em tempo de execução,
		 * é útil para testar enquanto estiver desenvolendo. Neste caso vou utilizar WindowsEditor para RuntimePlatform
		 * porque a plataforma utilizada para desenvolvimento é windows.
		 * 
		 * Obs.: altere o valor de RuntimePlatform de acordo com a plataforma que estiver utilizando	
		*/

		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{	
				Debug.Log ("Clicou no botão");
				CheckTouch(Input.mousePosition, "began");
			}

			if (Input.GetMouseButtonUp(0))
			{
				Debug.Log ("Soltou o botão do mouse");
				CheckTouch(Input.mousePosition, "ended");
			}
		}
	}

	/*
	 * Método executado quando uma das condições do botão é satisfeita: ao começar o toque na tela, ao acabar o toque na tela;
	 * 
	 * Recebe referências sobre posição do toque e o estado do toque e executa ações nos objetos da cena;
	 * 
	*/
	void CheckTouch(Vector3 pos, string phase)
	{
		Vector3 newPos = alien.GetComponent<Transform>().position;

		/*
		 * Cria-se um ponto do plano (a cena) através de uma posição recebida ao toque da tela;
		*/
		Vector3 point = Camera.main.ScreenToWorldPoint(pos);

		/*
		 * Pega as cordenas do ponto que o usuário tocou;
		 * 
		 */
		Vector2 position = new Vector2(point.x, point.y);

		/*
		 * Objeto com as informações sobre a colisão;
		 */
		Collider2D hit = Physics2D.OverlapPoint(position);

		//Se o botão for tocado

		if (hit.gameObject.name == "JumpButton" && hit && phase == "began")
		{
			alien.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 3f), ForceMode2D.Impulse); //Adiciona força ao pulo do player
			//GetComponent<AudioSource>().Play(); // toca o som fixado para esse objeto (no caso o pulo)
		}
	
	}
}
