# Freelanches-Game
Correção de bugs
- Texto aparece sempre mesmo carregando item, Qual condicao colocar pra evitar isso? ✔✔
- Arrumar a ordem dos colliders -> Fiz uma gambiarra (por enquanto) ✔✔
- Impedir de pegar o objeto enquanto ele esta caindo ✔✔
- Talvez colocar uma forma de soltar o item e destrui-lo quando cair?? -> Ao inves destrui-lo, ele dá respawn agora ✔✔
- UI da bancada aparecendo sempre ✔✔
- Ao item ser jogado pra frente a UI de pegar item esta aparecendo. Como retirar?? ✔✔
- Como pegar o pedido e desativar o botão de pega-lo de novo a partir do script QuadroButtomSystem?? ✔✔
  + Nesse caso, são um conjunto de pedidos e nao apenas um. Como fazer para guardar cada um dos pedidos em um array e dps saber qual foi pego ou não? ✔✔
- Bug sobre os prefabs dos ingredientes que eram para serem colocados como child de "Ingredientes" do resumo de pedidos, porém está dando um bug o qual nao sei corrigir
- Impedir que a pessoa jogue fora o pedido que já está pronto na montagem
- 


Implementar:
- Finalizar a funcao de segurar itens ✔✔
  - Colocar os itens no centro da bancada ao clicar para colocar ao inves de apenas deixa-lo naquela posicao? -> Funcao adicional ✔✔
  - Necessario um UI em cima do jogador mostrando qual o botao para largar o item enquanto o jogador nao possui nada na frente dele ✔✔

- Pop-out de uma tabela de pedidos ✔✔
  - Montar uma forma de pegar os pedidos com o mouse (pegar para mostrar na tela e o jogador poder ver o pedido, sem ter que abrir o quadro o tempo todo) ✔✔
  - Colocar o que vai ter dentro de cada pedido: ✔✔
    + Colocar pequenas imagens dos alimentos no papel do pedido ✔✔

- UI dos pedidos la em cima -> Lembrar que é um pedido por vez que é possivel ser preparado 
  - Resumo do pedido: ✔✔
    + Mostrar imagens (grandes) com cada um dos alimentos que devem ser entregues na bancada ✔✔
  - Tempo do pedido 
  - Pontuacao do pedido -> De acordo com a dificuldade do pedido e de acordo com o tempo que ele ficou sem ser preparado

- Criar uma "tabua" onde é possivel montar o pedido ✔✔

- Criar um lugar para finalizar o pedido (uma bancada de entrega) ✔✔
  - Sistema de pontuacao -> Aumentar a pontuacao do jogador de acordo com o pedido entregue

- Montar um UI com a pontuacao do jogador ate aquele momento ✔✔

- Contagem do tempo da fase ✔✔

- Criar cena do menu principal

- Cria cena da tela de fases

- Polir tela de pausa 

- Polir tela de fim da fase  

- Criar novos prefabs de pedidos

- Criar novos prefabs de ingredientes

- Criar UI mostrando onde entregar o ingrediente quando estiver com o ingrediente na mão e o pedido pego

- Criar UI para mostrar onde entregar o pedido quando o pedido já estiver completo

NOTAS: 
Existem 3 maneiras de carregar o item:
Primeiro: Pegar o item -> Prompt aparece com o collider em sua frente
Segundo: Soltar o item no chao -> Prompt aparece mesmo sem o collider em sua frente
Terceiro: Soltar o item na mesa -> Prompt aparece apenas com o item na mao e collider em sua frente
Quarto: Nao segurar nada e nao ter nada na frente -> Nao aparece nenhum prompt

Regra de negocios sobre os pedidos:
1- Pessoa abre o quadro de pedidos
2- Pessoa escolhe um pedido que possui certos ingredientes clicando nele
   + Exemplo: Pao, hamburguer, queijo, tomate, pao
3- Esse pedido irá aparecer la em cima no resumo dos pedidos


