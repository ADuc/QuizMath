# 🎮 QuizMath Game

Game trắc nghiệm toán học phát triển bằng **Unity 2021.1.6f1** và **Firebase 13.4.0**  
Hỗ trợ **login Gmail**, **lưu điểm**, **Leaderboard**.  
Git chỉ cập nhật thư mục **/Assets**.

---

## ⚙️ Thông tin

| Thành phần | Phiên bản |
|-------------|------------|
| Unity | 2021.1.6f1 |
| Firebase SDK | 13.4.0 |
| Target | WebGL / Android |

---

## 🧠 Gameplay

- Trả lời đúng → nút xanh  
- Trả lời sai → dừng game, hiện **GameOver Panel**  
- Có **Play Again** và **Leaderboard**

---

## 🔧 Cài đặt

1. Clone repo  
   ```bash
   git clone https://github.com/<yourname>/QuizMath.git
Mở bằng Unity 2021.1.6f1

Thêm file Firebase (google-services.json hoặc GoogleService-Info.plist) vào Assets/Plugins/Firebase/

🧾 Gitignore
javascript
Copy code
/[Ll]ibrary/
/[Tt]emp/
/[Oo]bj/
/[Bb]uild*/
/[Pp]ackages/
/UserSettings/
!.gitignore
!/Assets/
Tác giả: Lê Anh Đức — 2025
License: MIT
