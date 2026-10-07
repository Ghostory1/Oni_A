# Oni_A

Unity 기반 2D 액션 게임 프로젝트

## 유니티 버전 : Unity 6.3 LTS (6000.3.25f1)

## Development Progress

### Step 1 - State Machine & Player 기본 구조

- EntityState 기반 상태 시스템 구현
- StateMachine을 통한 상태 전환 구조 구현
- Player 기본 이동 및 입력 시스템 구현
- Idle / Move 상태 구현

### Step 2 - Jump & Airborne System

- Jump / Fall 상태 구현
- 공중 이동 및 점프 물리 구현
- Rigidbody2D 기반 속도 제어
- Ground Detection 구현

### Step 3 - Wall Slide

- Raycast 기반 Wall Detection 구현
- Wall Slide 상태 구현
- 벽에 붙은 상태에서 낙하 속도 조절
- 아래 입력을 통한 빠른 Wall Slide 구현
- Wall Slide 애니메이션 상태 연결

### Step 4 - Wall Slide Jump

- Wall Slide 상태에서 점프 기능 구현
- 벽의 반대 방향으로 점프하는 Wall Jump 구현
- Wall Jump 시 수평 / 수직 속도 제어
- Wall Jump 이후 공중 상태 전환 구현
- 벽 방향에 따른 점프 방향 처리

### Step 5 - Dash
- 대쉬 기능
- 행동 중 캔슬 대쉬 가능
- 공격 중 대쉬 방향 변경 가능

### Step 6 - Basic Attack

- Grounded 상태에서 기본 공격 기능 구현
- 공격 입력을 통한 Basic Attack 상태 전환
- Basic Attack 애니메이션 연결
- 공격 중 상태 전환 및 제어 구현
- 4콤보 어택 구현
- 각 콤보마다 앞으로 치고 나가는 AttackVelocity 배열로 변경
- lastTimeAttacked 타이머를 이용하여 콤보 초기화 진행
- 콤보 어택 사이사이 idleState로 넘어가는걸 Player에서 코루틴을 생성해서 조작감 상승
- 어택 중간중간 공격방향 변경 가능

## Current State Structure

```text
Player
└─ StateMachine
   ├─ GroundedState
   │  ├─ IdleState
   │  ├─ MoveState
   │  └─ BasicAttackState
   │
   ├─ JumpState
   ├─ FallState
   └─ WallSlideState



