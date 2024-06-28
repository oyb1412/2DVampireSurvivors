### 

# Loguelike


---

## Description

---


- 🔊프로젝트 소개

  Loguelike는 끊임없이 등장하는 적을 처치하고 성장하는 '뱀파이어 서바이벌'류 2D 게임입니다. 엔진의 기본 기능과 작동 원리, 주요 로직 구현에 중점을 두었습니다.

       

- 개발 기간 : 2024.12.21 - 2024.12.28

- 🛠️사용 기술

   -언어 : C#

   -엔진 : Unity Engine

   -데이터베이스 : 로컬

   -개발 환경: Windows 10, Unity 2021.3.10f1



- 💻구동 화면
![스크린샷(6)](https://github.com/oyb1412/2DVampireSurvivors/assets/154235801/463e9a50-6656-45bd-99ff-78201ee9fa13)

## 목차

---

- 기획 의도
- 핵심 로직


### 기획 의도

---

- 첫 프로젝트였기 때문에, 보다 기본적인 기능 위주의 구현

- 최적화보단 구현 위주

### 핵심 로직

---
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・플레이어 이동에 따른 맵 위치 동기화

플레이어가 맵을 벗어나는걸 콜라이더로 감지해, 플레이어의 위치와 맵의 위치를 동기화

![그림1](https://github.com/oyb1412/2DVampireSurvivors/assets/154235801/2fd50f93-4a8c-48b6-a8d1-4ef18b78e86c)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)


### ・하나의 프리펩으로 여러 애너미 관리

형태가 다르지만 행동은 같은 애너미들을 하나의 프리펩, 하나의 스크립트로 관리

![그림2](https://github.com/oyb1412/2DVampireSurvivors/assets/154235801/62af38a1-0ac3-4c84-8cf7-4c82ea36edca)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・각종 무기 및 패시브형 스킬 구현

직접적으로 적을 공격하는 무기 및 능력치를 상승시켜주는 패시브형 스킬 구현

![그림3](https://github.com/oyb1412/2DVampireSurvivors/assets/154235801/55065d4c-73ca-453a-9e65-c58772d3b75f)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・최적화를 위한 풀링 오브젝트

객체 생성 및 제거로 인한 퍼포먼스 저하 문제를 해결하기 위해 풀링 시스템 사용.

![화면 캡처 2024-06-28 231539](https://github.com/oyb1412/2DVampireSurvivors/assets/154235801/4fa0de94-b36d-4482-a514-869c0ba51e25)
