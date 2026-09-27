# Oni_A

Unity 기반 2D 액션 게임 프로젝트

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

## Current State Structure

```text
Player
└─ StateMachine
   ├─ GroundedState
   │  ├─ IdleState
   │  └─ MoveState
   │
   ├─ JumpState
   ├─ FallState
   └─ WallSlideState
Input
Input	Action
WASD	Movement
Space	Jump
S	Fast Wall Slide
Tech Stack
Unity
C#
Unity Input System
Rigidbody2D
Animator
State Machine Pattern
Raycast



