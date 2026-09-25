# Oni\_A

\# Oni\_A



Unity 기반 게임 프로젝트



\## Development Progress



\### Step 1 - State Machine \& Player 기본 구조



\#### 구현 내용



\- `EntityState`

&#x20; - Entity의 상태를 정의하기 위한 기본 State 클래스

&#x20; - 상태의 공통적인 동작 구조 정의



\- `StateMachine`

&#x20; - 현재 State를 관리

&#x20; - State 전환 처리



\- `Player`

&#x20; - Player의 기본 구조 생성

&#x20; - StateMachine을 이용한 상태 관리 기반 마련



\- `Player\_IdleState`

&#x20; - Player의 대기 상태 구현



\- `Player\_MoveState`

&#x20; - Player의 이동 상태 구현



\- `PlayerInputSystemSet`

&#x20; - Player Input System 구성

&#x20; - Player의 입력을 상태와 연결하기 위한 기반 구현

&#x20; -   Vector2 - Movement - WASD



\#### 현재 State 구조



```text

Player

&#x20;└─ StateMachine

&#x20;    ├─ Player\_IdleState

&#x20;    └─ Player\_MoveState

