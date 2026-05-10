using System.Text;
using DuelRecords.Blazor.Models;


namespace DuelRecords.Services;

public static class CardArtBuilder
{
    private static int HashStr(string s)
    {
        int h = 0;
        foreach (var ch in s) h = unchecked(((h << 5) - h) + ch);
        return Math.Abs(h);
    }

    public static string ArtSvg(Card card)
    {
        int seed = HashStr(card.Name);
        var palette = (card.Attribute != null && CardCatalog.ArtPalettes.TryGetValue(card.Attribute, out var p))
            ? p : new[] { "#6d28d9", "#3b1080", "#0a0312" };

        double R(int n) => ((seed * (n + 1)) % 100) / 100.0;

        var sb = new StringBuilder();

        if (card.Type == "Magia")
        {
            sb.Append($@"<svg viewBox=""0 0 100 100"" xmlns=""http://www.w3.org/2000/svg"" preserveAspectRatio=""xMidYMid slice"">
                <defs><radialGradient id=""g{seed}"" cx=""50%"" cy=""50%"" r=""60%"">
                    <stop offset=""0%"" stop-color=""#10b981"" stop-opacity=""1""/>
                    <stop offset=""50%"" stop-color=""#0d9488"" stop-opacity=""0.8""/>
                    <stop offset=""100%"" stop-color=""#052e2b"" stop-opacity=""1""/>
                </radialGradient></defs>
                <rect width=""100"" height=""100"" fill=""url(#g{seed})""/>");
            for (int i = 0; i < 12; i++)
            {
                double a = (i / 12.0) * Math.PI * 2;
                double x1 = 50 + Math.Cos(a) * 12, y1 = 50 + Math.Sin(a) * 12;
                double x2 = 50 + Math.Cos(a) * (38 + R(i) * 8), y2 = 50 + Math.Sin(a) * (38 + R(i) * 8);
                sb.Append($@"<line x1=""{x1:F2}"" y1=""{y1:F2}"" x2=""{x2:F2}"" y2=""{y2:F2}"" stroke=""rgba(255,255,255,0.35)"" stroke-width=""0.6""/>");
            }
            sb.Append(@"<circle cx=""50"" cy=""50"" r=""14"" fill=""none"" stroke=""rgba(255,255,255,0.6)"" stroke-width=""0.8""/>
                <circle cx=""50"" cy=""50"" r=""8"" fill=""rgba(255,255,255,0.18)""/>
                <text x=""50"" y=""54"" text-anchor=""middle"" font-family=""Cinzel,serif"" font-size=""12"" font-weight=""700"" fill=""rgba(255,255,255,0.85)"">魔</text></svg>");
            return sb.ToString();
        }

        if (card.Type == "Armadilha")
        {
            sb.Append($@"<svg viewBox=""0 0 100 100"" xmlns=""http://www.w3.org/2000/svg"" preserveAspectRatio=""xMidYMid slice"">
                <defs><radialGradient id=""g{seed}"" cx=""50%"" cy=""50%"" r=""70%"">
                    <stop offset=""0%"" stop-color=""#ec4899"" stop-opacity=""1""/>
                    <stop offset=""60%"" stop-color=""#831843"" stop-opacity=""0.9""/>
                    <stop offset=""100%"" stop-color=""#1c0a14"" stop-opacity=""1""/>
                </radialGradient></defs>
                <rect width=""100"" height=""100"" fill=""url(#g{seed})""/>");
            for (int i = 0; i < 8; i++)
            {
                double a = (i / 8.0) * Math.PI * 2 + R(i);
                double x = 50 + Math.Cos(a) * (15 + R(i) * 25);
                double y = 50 + Math.Sin(a) * (15 + R(i) * 25);
                sb.Append($@"<polygon points=""{x:F2},{y - 4:F2} {x + 3:F2},{y + 3:F2} {x - 3:F2},{y + 3:F2}"" fill=""rgba(255,255,255,0.35)""/>");
            }
            sb.Append(@"<text x=""50"" y=""56"" text-anchor=""middle"" font-family=""Cinzel,serif"" font-size=""14"" font-weight=""700"" fill=""rgba(255,255,255,0.9)"">罠</text></svg>");
            return sb.ToString();
        }

        // Monster
        int silhouette = seed % 4;
        string creature = silhouette switch
        {
            0 => @"<path d=""M 22 70 Q 30 45 50 35 Q 70 28 80 50 Q 76 60 68 58 Q 60 56 56 65 Q 52 75 42 72 Q 32 70 22 70 Z"" fill=""rgba(0,0,0,0.55)""/>
                  <path d=""M 30 64 Q 38 50 50 44 Q 62 40 70 52"" stroke=""rgba(255,255,255,0.4)"" stroke-width=""0.8"" fill=""none""/>
                  <circle cx=""74"" cy=""46"" r=""1.6"" fill=""#fef3c7""/>",
            1 => @"<path d=""M 50 24 L 60 36 L 64 64 L 60 78 L 40 78 L 36 64 L 40 36 Z"" fill=""rgba(0,0,0,0.55)""/>
                  <circle cx=""50"" cy=""32"" r=""6"" fill=""rgba(0,0,0,0.55)""/>
                  <path d=""M 38 28 L 50 14 L 62 28"" stroke=""rgba(255,255,255,0.5)"" stroke-width=""1"" fill=""none""/>
                  <circle cx=""50"" cy=""50"" r=""2.4"" fill=""#fef3c7""/>",
            2 => @"<rect x=""40"" y=""30"" width=""20"" height=""44"" rx=""4"" fill=""rgba(0,0,0,0.55)""/>
                  <path d=""M 32 40 L 40 36 L 40 60 L 32 64 Z"" fill=""rgba(0,0,0,0.55)""/>
                  <path d=""M 60 36 L 68 40 L 68 64 L 60 60 Z"" fill=""rgba(0,0,0,0.55)""/>
                  <rect x=""46"" y=""22"" width=""8"" height=""14"" fill=""rgba(0,0,0,0.55)""/>
                  <rect x=""48"" y=""26"" width=""4"" height=""2"" fill=""#fef3c7""/>",
            _ => @"<ellipse cx=""50"" cy=""58"" rx=""22"" ry=""14"" fill=""rgba(0,0,0,0.55)""/>
                  <circle cx=""50"" cy=""40"" r=""11"" fill=""rgba(0,0,0,0.55)""/>
                  <polygon points=""42,32 38,22 46,28"" fill=""rgba(0,0,0,0.55)""/>
                  <polygon points=""58,32 62,22 54,28"" fill=""rgba(0,0,0,0.55)""/>
                  <circle cx=""46"" cy=""40"" r=""1.4"" fill=""#fef3c7""/>
                  <circle cx=""54"" cy=""40"" r=""1.4"" fill=""#fef3c7""/>",
        };

        sb.Append($@"<svg viewBox=""0 0 100 100"" xmlns=""http://www.w3.org/2000/svg"" preserveAspectRatio=""xMidYMid slice"">
            <defs>
                <linearGradient id=""bg{seed}"" x1=""0"" y1=""0"" x2=""0"" y2=""1"">
                    <stop offset=""0%"" stop-color=""{palette[0]}""/>
                    <stop offset=""60%"" stop-color=""{palette[1]}""/>
                    <stop offset=""100%"" stop-color=""{palette[2]}""/>
                </linearGradient>
                <radialGradient id=""vig{seed}"" cx=""50%"" cy=""60%"" r=""80%"">
                    <stop offset=""0%"" stop-color=""rgba(255,255,255,0.18)""/>
                    <stop offset=""80%"" stop-color=""rgba(0,0,0,0)""/>
                </radialGradient>
            </defs>
            <rect width=""100"" height=""100"" fill=""url(#bg{seed})""/>");
        for (int i = 0; i < 6; i++)
        {
            double a = (i / 6.0) * Math.PI * 2;
            double cx = 50 + Math.Cos(a) * 40;
            double cy = 50 + Math.Sin(a) * 40;
            double r = 1 + R(i) * 2;
            double op = 0.2 + R(i) * 0.5;
            sb.Append($@"<circle cx=""{cx:F2}"" cy=""{cy:F2}"" r=""{r:F2}"" fill=""rgba(255,255,255,{op:F2})""/>");
        }
        sb.Append(creature);
        sb.Append($@"<rect width=""100"" height=""100"" fill=""url(#vig{seed})""/></svg>");
        return sb.ToString();
    }
}
