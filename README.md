QuizMath — Unity Game
🧩 Project Overview

QuizMath là một trò chơi hỏi đáp toán học được phát triển bằng Unity 2021.1.6f1. Người chơi sẽ trả lời các câu hỏi trắc nghiệm toán học trong giới hạn thời gian để ghi điểm cao nhất.

🎯 Features

Hệ thống câu hỏi toán học ngẫu nhiên (cộng, trừ, nhân, chia)

Giao diện thân thiện, dễ mở rộng

Bộ đếm thời gian (Timer) cho mỗi câu hỏi

Hệ thống tính điểm và lưu điểm cao (High Score)

Âm thanh và hiệu ứng khi trả lời đúng/sai

🛠️ Development Environment

Unity version: 2021.1.6f1

Platform target: PC, Android (optional)

Render Pipeline: Built-in

📁 Project Structure
Assets/
  ├── Scripts/               # Chứa các file C# script logic
  │   ├── GameManager.cs     # Quản lý vòng chơi, điểm, câu hỏi
  │   ├── QuestionGenerator.cs # Sinh câu hỏi ngẫu nhiên
  │   ├── UIController.cs    # Quản lý giao diện và nút bấm
  │   └── SoundManager.cs    # Quản lý hiệu ứng âm thanh
  │
  ├── Prefabs/               # Lưu trữ prefab UI, button, question box
  ├── Scenes/                # Scene chính: QuizMath.unity
  ├── Audio/                 # Âm thanh phản hồi đúng/sai
  ├── Sprites/               # Ảnh giao diện, icon
  └── Fonts/                 # Font chữ dùng cho UI


# Các thư mục sau KHÔNG upload lên GitHub
Library/
Logs/
Temp/
Obj/
Build/
UserSettings/

⚠️ Khi upload lên GitHub, chỉ upload thư mục Assets/ và file ProjectSettings/ cần thiết.

🧮 How to Run

Mở Unity Hub → chọn Unity 2021.1.6f1

Chọn Open Project → trỏ đến thư mục gốc của QuizMath

Mở scene chính tại: Assets/Scenes/QuizMath.unity

Nhấn ▶ (Play) để chạy trò chơi

📜 How to Play

Khi bắt đầu, màn hình sẽ hiển thị câu hỏi toán học.

Chọn một trong các đáp án A/B/C/D.

Trả lời đúng → cộng điểm. Sai → trừ điểm hoặc kết thúc tùy chế độ.

Hết thời gian → tự động chuyển sang câu hỏi tiếp theo.

🧰 Scripts Overview
GameManager.cs

Quản lý trạng thái trò chơi (đang chơi, kết thúc, khởi động lại)

Cập nhật điểm và high score

QuestionGenerator.cs

Sinh phép toán ngẫu nhiên (±, ×, ÷)

Đảm bảo kết quả hợp lệ

UIController.cs

Hiển thị câu hỏi và đáp án

Xử lý sự kiện click của người chơi

SoundManager.cs

Phát âm thanh khi trả lời đúng/sai

Quản lý nhạc nền (optional)

🧾 Git & Upload Instructions

Tạo .gitignore file với nội dung sau:

[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
UserSettings/
MemoryCaptures/
Packages/com.unity.timeline/

Commit và push lên GitHub:

git init
git add Assets ProjectSettings .gitignore
git commit -m "Initial commit - QuizMath game"
git branch -M main
git remote add origin <your_repo_url>
git push -u origin main
📦 Optional Improvements

Thêm hệ thống câu hỏi cấp độ (dễ, trung bình, khó)

Tích hợp lưu trữ online leaderboard

Hỗ trợ đa ngôn ngữ (VN/EN)

Thêm hiệu ứng particle khi trả lời đúng

📄 License

MIT License © 2025 Bạn được phép sao chép, chỉnh sửa và phân phối với điều kiện giữ nguyên thông tin tác giả gốc.
