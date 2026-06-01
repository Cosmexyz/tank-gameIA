# Tank-GameIA - Documentação do Projeto

## 📋 Visão Geral
**Tank-GameIA** é um jogo de tanques 3D desenvolvido em **Unity 2022.3.62f3** com sistema de IA para inimigos usando NavMesh.

## 🎮 Funcionalidades Principais

### Sistema de Movimento
- **MovimentoTanqueStanley**: Controla o tanque do jogador via teclado (W/A/S/D)
- **CameraTopDown**: Câmera em primeira pessoa que segue o jogador
- Limites de terreno implementados

### Sistema de Combate
- **MovimentoTorre**: Rotação da torre do tanque controlada pelo mouse
- **MovimentoMira**: Movimento vertical da mira (pitch)
- **TankVida**: Sistema de vida com barra visual

### Sistema de IA
- **EnemyAI**: IA básica para inimigos com:
  - Patrulha automática entre pontos definidos
  - Detecção do jogador em raio de 20 metros
  - Perseguição quando jogador detectado
  - Ataque quando dentro de 5 metros

### Gerenciamento do Jogo
- **GameManager**: Controla menu, início do jogo, pausa e game over
- **Menuinicial**: Menu inicial do jogo
- **TanqueSpawnpoint**: Sistema de spawn de tanques

## 📁 Estrutura de Pastas

```
Unity_game261/
├── Assets/
│   ├── Main Code/
│   │   └── Tank_Base.cs (classe base para tanques)
│   ├── Tanques/
│   │   ├── MovimentoTanqueStanley.cs
│   │   ├── MovimentoTorre.cs
│   │   ├── MovimentoMira.cs
│   │   ├── CameraTopDown.cs
│   │   ├── TankVida.cs
│   │   ├── GameManager.cs
│   │   ├── TanqueSpawnpoint.cs
│   │   └── Menuinicial.cs
│   ├── EnemyAI/ (IA dos inimigos)
│   │   └── EnemyAI.cs
│   ├── Antunes/
│   │   └── MoverTeste01.cs
│   ├── PowerUps/
│   ├── Scenes/
│   │   ├── SampleScene1.0.unity (principal)
│   │   └── Tank_antunes.unity
│   └── Materiais/
└── ProjectSettings/
```

## 🔧 Scripts Principais

### TankBase
Classe base para todos os tanques com:
- Gestão de vida
- Sistema de dano
- Método virtual `Die()`
- Propriedades compartilhadas

```csharp
public class TankBase : MonoBehaviour
{
    public virtual void TakeDamage(float damage);
    public virtual void Die();
}
```

### EnemyAI (NavMeshAgent.cs)
Sistema de IA com dois modos:
1. **Patrulha**: Movimento automático entre pontos-chave
2. **Perseguição**: Segue o jogador quando detectado

**Parâmetros configuráveis:**
- `detectionRange`: 20m (raio de detecção)
- `attackRange`: 5m (distância de ataque)
- `patrolSpeed`: 3.5 m/s
- `chaseSpeed`: 5 m/s

### TankVida
Sistema de vida com barra visual:
- Gerencia HP do tanque
- Barra de vida com UI Slider
- Morte automática ao chegar a 0 HP
- Teste com tecla H

### GameManager
Controla o estado do jogo:
- Início/Pausa/Retomada
- Ativação de elementos da UI
- Game Over

## 🎯 Como Usar

### Configurar Inimigos
1. Crie um tanque inimigo na cena
2. Adicione o script **EnemyAI** ao tanque
3. Configure os pontos de patrulha como filhos do inimigo
4. Marque o jogador com a tag "Player"

### Adicionar Novo Tanque
1. Herde de **TankBase**
2. Implemente métodos de movimento customizados
3. Utilize `TakeDamage()` para dano
4. Utilize `Die()` para destruição

## ⚙️ Padrões de Código

### Convenções de Nomes
- Classes: **PascalCase** (ex: `MovimentoTorre`)
- Métodos públicos: **PascalCase**
- Campos privados: **camelCase**
- Constantes: **UPPER_SNAKE_CASE**

### Gerenciamento de Objetos
- Use `[SerializeField]` ao invés de `public` para dados que precisam ser atribuídos no inspector
- Evite `Resources.FindObjectsOfTypeAll()` - use referências diretas no inspector
- Use tags (ex: "Player") para encontrar objetos importantes

## 🐛 Conhecidas Limitações

- IA não tem sistema de ataque implementado
- Sem sistema de save/load
- Sem suporte para multiplayer
- Power-ups Pedra-Papel-Tesoura não integrados com IA

## 🚀 Melhorias Futuras

- [ ] Implementar sistema de ataque para IA
- [ ] Adicionar efeitos sonoros
- [ ] Criar UI de pausa
- [ ] Implementar sistema de pontuação
- [ ] Adicionar power-ups funcionais
- [ ] Otimizar performance com object pooling

## 📊 Status do Projeto

**Versão:** 0.2 (Em Desenvolvimento)  
**Último Update:** 2026-05-29  
**Contribuidores:** Willian, Cosme, Stanley

---

Para questões de desenvolvimento, consulte os comentários nos scripts individuais.
