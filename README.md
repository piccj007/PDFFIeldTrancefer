# 📄 PDF Field Transfer Engine (.NET)

## 🚀 Overview

This project is a **PDF Form Structure Rebuilder** built using **.NET + iText7**.

It extracts form field metadata (currently **width & height**) from an existing PDF and recreates the same field structure in a new PDF.

> ⚡ Current Mode: **Size-Only Mapping (Position Ignored)**

---

## 🎯 Features

* ✅ Extract field **width & height** from source PDF
* ✅ Rebuild form fields in new PDF
* ✅ Preserve field types:

  * Text Fields
  * Checkboxes
  * Dropdowns
* ✅ Clean and modular architecture
* ❌ Position (X, Y) intentionally ignored (for now)

---

## 🧠 Architecture

```
Old PDF
   ↓
PdfFieldSizeExtractor
   ↓
Dictionary<string, (width, height)>
   ↓
FieldStructureBuilder
   ↓
New PDF (with correct sizes)
```

---

## 📂 Project Structure

```
/ProjectRoot
│
├── PdfFieldSizeExtractor.cs   // Extracts width & height
├── FieldStructureBuilder.cs   // Builds new PDF structure
├── Program.cs                 // Entry point
├── old.pdf                    // Source PDF
├── new.pdf                    // Target template
└── output.pdf                 // Final output
```

---

## ⚙️ How It Works

### 1️⃣ Extract Field Sizes

```csharp
var sizeMap = PdfFieldSizeExtractor.Extract(oldPdfPath);
```

* Reads all form fields
* Stores width & height in a dictionary

---

### 2️⃣ Build New Structure

```csharp
FieldStructureBuilder.Build(oldPdf, newPdf, outputPdf);
```

* Iterates over old fields
* Applies extracted width & height
* Uses dummy position `(10,10)` (ignored layout)

---

## ⚠️ Important Notes

* PDF fields **require position**, even if ignored
* Currently, all fields overlap (same position)
* This is intentional for **size validation phase**

---

## 📸 Current Output

* All fields are created successfully
* Width & height match original PDF
* Fields overlap due to ignored positioning

---

## 🔮 Next Steps (Roadmap)

* 🔜 Extract and apply **field positions (X, Y)**
* 🔜 Build **layout engine**
* 🔜 Add **AI-based field alignment**
* 🔜 Generate HTML forms from PDF
* 🔜 JSON-based dynamic field mapping

---

## 🛠 Tech Stack

* .NET (C#)
* iText7 PDF Library

---

## 💡 Use Cases

* PDF Migration Tools
* Form Automation Systems
* AI Document Processing
* Dynamic Form Generation

---

## 👨‍💻 Author

Ravi Suthar

---

## ⭐ Final Note

This project is evolving into a **PDF Reverse Engineering Engine**,
starting from structure → moving towards full layout and automation.

---
