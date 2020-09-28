// JavaScript source code
var champion = [
    "Apothecary",
    "Arbiter",
    "Broadmaw",
    "Diabolist",
    "Elder Skarg",
    "Flesh-Tearer",
    "Foli",
    "Frostbringer",
    "Galek",
    "Golden Reaper",
    "Gorgorab",
    "Haruspex",
    "Heiress",
    "Hellgazer",
    "Hexia",
    "Hexweaver",
    "High Khatun",
    "Hope",
    "Hordin",
    "Juliana",
    "Krisk",
    "Kymar",
    "Lanakis",
    "Longbeard", "Lyssandra", "Marksman", "Martyr", "MaShalled", "Maulie", "Painkeeper", "Pitiless", "Sandlashed Survivor", "Seeker", "Sethallia", "Shatterbones", "Shirimani", "Sikara", "Siphi", "Skullcrusher", "Turvold", "Valerie", "Valla", "Valkyrie", "Vrask", "Ultimate Galek",];

function generate_select() {
    for (var a = "", e = 0; e < champion.length; e++)
        "MaShalled" == champion[e] ? (a += '<option value="' + champion[e] + "\">Ma'Shalled</option>") : (a += '<option value="' + champion[e] + '">' + champion[e] + "</option>");
    $(".champion_select").each(function () { $(this).append(a) })
}

new AutoNumeric.multiple(
    "input#speed_aura_bonus",
    {
        suffixText: "%",
        digitGroupSeparator: " ",
        decimalCharacter: ".",
        decimalPlaces: 0,
        minimumValue: 0,
        maximumValue: 100,
    }),
    $(document).on("mouseenter", ".tooltip_sign", function () {
        jQuery(this).parent(".name").find(".tooltip").css("display", "block")
    }),
    $(document).on("mouseleave", ".tooltip_sign", function () {
        jQuery(this).parent(".name").find(".tooltip").css("display", "none")
    }),
    $(document).ready(function () {
            generate_select(),
            $(".select").not(".champion_select").select2({
                minimumResultsForSearch: -1,
                width: "100%"
            }),
            $(".champion_select").select2({
                width: "100%",
                tags: !0,
                createTag: function (a) { return { id: a.term, text: a.term, newOption: !0 } },
            }),
            $(".help").click(function () {
                $(".info").toggle()
            }),
            $(".cooldown_4, .cooldown_3, .cooldown_2, .delay_4, .delay_3, .delay_2").change(function () {
                parseFloat($(this).val()) > 10 && $(this).val(10)
            }),
            $(".cooldown_4, .cooldown_3, .cooldown_2, .delay_4, .delay_3, .delay_2").change(function () {
                parseFloat($(this).val()) < 0 && $(this).val(0)
            }),
            $("input").change(function () {
                calculator()
            }),
            $("select").change(function () {
                calculator()
            }),
            calculator()
    }),
    (Number.prototype.formatMoney = function (a, e, s) {
        var l = this, o = ((a = isNaN((a = Math.abs(a))) ? 2 : a), (e = null == e ? "." : e), (s = null == s ? "," : s), l < 0 ? "-" : ""), n = parseInt((l = Math.abs(+l || 0).toFixed(a))) + "", d = (d = n.length) > 3 ? d % 3 : 0;
        return (o + (d ? n.substr(0, d) + s : "") + n.substr(d).replace(/(\d{3})(?=\d)/g, "$1" + s) + (a ? e + Math.abs(l - n).toFixed(a).slice(2) : ""))
    });

var long_ = [];

function calcSpeedBonus(baseSpeed, speedSets, hasLore) {
    if (hasLore) {
        return Math.round(baseSpeed * speedSets * 0.12 * 0.15)
    } else {
        return Math.round(baseSpeed * speedSets * 0.12)
    }
}

function calculator()
{
    var a = "", e = "", magicSpeedNumber = 1428.571429; // The magic number is 10000/7

    function l(a)
    {
        var e = [];
        return ($("." + a).each(function () {
            "base_speed" == a ? e.push(parseFloat($(this).val()) || 0) : e.push(parseFloat($(this).val()) || "")
        }), e)
    }

    var o = {
        boss: "Clan Boss",
        difficulty: $("#difficulty option:selected").val(),
        difficulty_val: $("#difficulty option:selected").data("value"),
        speed_aura_bonus: parseFloat(AutoNumeric.getNumber("#speed_aura_bonus")) / 100 || 0,
        champions: [
            $("#champion_1 option:selected").val(),
            $("#champion_2 option:selected").val(),
            $("#champion_3 option:selected").val(),
            $("#champion_4 option:selected").val(),
            $("#champion_5 option:selected").val(),
        ],
        lore: [
            $(".lore_steel").eq(0).find(" option:selected").val(),
            $(".lore_steel").eq(1).find(" option:selected").val(),
            $(".lore_steel").eq(2).find(" option:selected").val(),
            $(".lore_steel").eq(3).find(" option:selected").val(),
            $(".lore_steel").eq(4).find(" option:selected").val(),
        ],
        speed_sets: l("speed_sets"),
        base_speed: l("base_speed"),
        total_speed: l("total_speed"),
        cooldown_4: l("cooldown_4"),
        cooldown_3: l("cooldown_3"),
        cooldown_2: l("cooldown_2"),
        delay_4: l("delay_4"),
        delay_3: l("delay_3"),
        delay_2: l("delay_2"),
    }, championEffectiveSpeed = []; // champion effective speeds

    !(function () {
        for (var a = 0; a < o.champions.length; a++) {
            var e = o.total_speed[a] - o.base_speed[a] - Math.round(o.base_speed[a] * o.speed_sets[a] * 0.12) - ("Yes" == o.lore[a] ? Math.round(o.base_speed[a] * o.speed_sets[a] * 0.12 * 0.15) : 0) +
                o.base_speed[a] + o.base_speed[a] * o.speed_sets[a] * 0.12 + ("Yes" == o.lore[a] ? o.base_speed[a] * o.speed_sets[a] * 0.12 * 0.15 : 0) + o.base_speed[a] * o.speed_aura_bonus;
            "number" == typeof e ? championEffectiveSpeed.push(e) : championEffectiveSpeed.push("")
        }

        for (a = 0; a < championEffectiveSpeed.length; a++)
            "number" == typeof championEffectiveSpeed[a] ? $(".calc_speed").eq(a).html(championEffectiveSpeed[a].formatMoney(2, ".", ",")) : $(".calc_speed").eq(a).html(0);
    })();

    var d = {
        a: ["null", "null", "null", "null", 1],
        clanBossTurnMeter: [o.difficulty_val / magicSpeedNumber, o.difficulty_val / magicSpeedNumber],
        c: ["null", "null"],
        champ0TurnMeter: [championEffectiveSpeed[0] / magicSpeedNumber, championEffectiveSpeed[0] / magicSpeedNumber],
        e: [o.cooldown_4[0] < 1 ? "null" : o.delay_4[0] + 1],
        f: [o.cooldown_3[0] < 1 ? "null" : o.delay_3[0] + 1],
        g: [o.cooldown_2[0] < 1 ? "null" : o.delay_2[0] + 1],
        h: ["null"],
        champ1TurnMeter: [championEffectiveSpeed[1] / magicSpeedNumber, championEffectiveSpeed[1] / magicSpeedNumber],
        j: [o.cooldown_4[1] < 1 ? "null" : o.delay_4[1] + 1],
        k: [o.cooldown_3[1] < 1 ? "null" : o.delay_3[1] + 1],
        l: [o.cooldown_2[1] < 1 ? "null" : o.delay_2[1] + 1],
        m: ["null"],
        champ2TurnMeter: [championEffectiveSpeed[2] / magicSpeedNumber, championEffectiveSpeed[2] / magicSpeedNumber],
        o: [o.cooldown_4[2] < 1 ? "null" : o.delay_4[2] + 1],
        p: [o.cooldown_3[2] < 1 ? "null" : o.delay_3[2] + 1],
        q: [o.cooldown_2[2] < 1 ? "null" : o.delay_2[2] + 1],
        r: ["null"],
        champ3TurnMeter: [championEffectiveSpeed[3] / magicSpeedNumber, championEffectiveSpeed[3] / magicSpeedNumber],
        t: [o.cooldown_4[3] < 1 ? "null" : o.delay_4[3] + 1],
        u: [o.cooldown_3[3] < 1 ? "null" : o.delay_3[3] + 1],
        v: [o.cooldown_2[3] < 1 ? "null" : o.delay_2[3] + 1],
        w: ["null"],
        champ4TurnMeter: [championEffectiveSpeed[4] / magicSpeedNumber, championEffectiveSpeed[4] / magicSpeedNumber],
        y: [o.cooldown_4[4] < 1 ? "null" : o.delay_4[4] + 1],
        z: [o.cooldown_3[4] < 1 ? "null" : o.delay_3[4] + 1],
        aa: [o.cooldown_2[4] < 1 ? "null" : o.delay_2[4] + 1],
        ab: ["null"],
        clanBossTurnByTableRow: [0],      // Clan boss turn number for each row in the table.
        championByTableRow: ["null"], // Array of the champion that acts for each row in the table.
        championSkillByTableRow: ["null"], // Array of the skill used by the champion for each row in the table.
        ag: [0],
        teamTurnMeterFillFromCurrentSkillByTableRow: [0],
        sikaraPercentTurnMeterFillByTableRow: [0],
        painkeeperPercentTurnMeterFillByTableRow: [0],
        ultimateGalekPercentTurnMeterFillByTableRow: [0],
        vallaPercentTurnMeterFillByTableRow: [0],
        vraskPercentTurnMeterFillByTableRow: [0],
        julianaPercentTurnMeterFillByTableRow: [0],
        hellgazerPercentTurnMeterFillByTableRow: [0],
        champ0SmallSpeedBoostByTableRow: [0],
        champ0BigSpeedBoostByTableRow: [0],
        ar: [0],
        champ1SmallSpeedBoostByTableRow: [0],
        champ1BigSpeedBoostByTableRow: [0],
        au: [0],
        champ2SmallSpeedBoostByTableRow: [0],
        champ2BigSpeedBoostByTableRow: [0],
        ax: [0],
        champ3SmallSpeedBoostByTableRow: [0],
        champ3BigSpeedBoostByTableRow: [0],
        ba: [0],
        champ4SmallSpeedBoostByTableRow: [0],
        champ4BigSpeedBoostByTableRow: [0],
        bd: [0],
        be: [0],
        bf: [0],
        bg: [0],
        bh: [0],
    };

    $("#table").html("");
    var i = '<tr><th><div class="name"><span class="rotate">Turn # ↓</span><div class="tooltip_sign"><i class="fa fa-question-circle-o" aria-hidden="true"></i></div><div class="tooltip">Turns the Clan Boss has taken.</div></div></th><th><div class="name">Turn Order<div class="tooltip_sign"><i class="fa fa-question-circle-o" aria-hidden="true"></i> </div> <div class="tooltip">Shows the order which each Champion/Clan Boss take</div> </div> </th> <th> <div class="name"> Skill Used <div class="tooltip_sign"> <i class="fa fa-question-circle-o" aria-hidden="true"></i> </div> <div class="tooltip">Skill used by the accompaning Champion</div> </div> </th> </tr>   ';

    function t(a, e, s, l, n, i, t) {
        o.cooldown_4[a] < 1 ? s.push("null") :
            d.championByTableRow[t] == o.champions[e] ? s.push(s[t - 1] - 1 - d.ag[t - 1]) :
                "a4" == i[t - 1] ? s.push(o.cooldown_4[a] - d.ag[t - 1]) :
                    s.push(s[t - 1] - d.ag[t - 1]),
            o.cooldown_3[a] < 1 ? l.push("null") :
                d.championByTableRow[t] == o.champions[e] ? l.push(l[t - 1] - 1 - d.ag[t - 1]) :
                    "a3" == i[t - 1] ? l.push(o.cooldown_3[a] - d.ag[t - 1]) :
                        l.push(l[t - 1] - d.ag[t - 1]),
            o.cooldown_2[a] < 1 ? n.push("null") :
                d.championByTableRow[t] == o.champions[e] ? n.push(n[t - 1] - 1 - d.ag[t - 1]) :
                    "a2" == i[t - 1] ? n.push(o.cooldown_2[a] - d.ag[t - 1]) :
                        n.push(n[t - 1] - d.ag[t - 1]),
            d.championByTableRow[t] == o.champions[e] ? s[t] <= 0 && "null" != s[t] ? i.push("a4") :
                l[t] <= 0 && "null" != l[t] ? i.push("a3") :
                    n[t] <= 0 && "null" != n[t] ? i.push("a2") :
                        i.push("a1") :
            i.push("null")
    }

    function set20PctTurnMeterFillByTableRow(a, e, s) {
        // a: champion name
        // e: output array
        // s: table row
        var l = (
            "Longbeard" == d.championByTableRow[s] && "a3" == d.championSkillByTableRow[s]) || // ally attack on this turn
            (d.championByTableRow[s] == a && "a1" == d.championSkillByTableRow[s]) || // champion used their A1 on this turn
            (((o.champions[0] == a && d.ar[s - 1] > 0) || (o.champions[1] == a && d.au[s - 1] > 0) || (o.champions[2] == a && d.ax[s - 1] > 0) || (o.champions[3] == a && d.ba[s - 1] > 0) || (o.champions[4] == a && d.bd[s - 1] > 0)) && d.championByTableRow[s] == o.boss && ("AOE 1" == d.championSkillByTableRow[s] || "AOE 2" == d.championSkillByTableRow[s]))
            ? 0.2 : 0;
        e.push(l)
    }

    function setPercentTurnMeterFillFromA1OnTableRowInArray(a, e, s, l) {
        // a: champion name
        // e: percent turn meter fill on A1
        // s: champion percent turn meter fill on this turn array
        // l: table row
        var n = ("Longbeard" == d.championByTableRow[l] && "a3" == d.championSkillByTableRow[l]) ||
            (d.championByTableRow[l] == a && "a1" == d.championSkillByTableRow[l]) ||
            (((o.champions[0] == a && d.ar[l - 1] > 0) || (o.champions[1] == a && d.au[l - 1] > 0) || (o.champions[2] == a && d.ax[l - 1] > 0) || (o.champions[3] == a && d.ba[l - 1] > 0) || (o.champions[4] == a && d.bd[l - 1] > 0)) && d.championByTableRow[l] == o.boss && ("AOE 1" == d.championSkillByTableRow[l] || "AOE 2" == d.championSkillByTableRow[l]))
            ? e : 0;
        s.push(n)
    }

    function u(a, e, s) {
        var l = d.be[s] >= a[s - 1] ? d.be[s] : (d.championByTableRow[s] == o.champions[e] ? a[s - 1] - 1 : a[s - 1]) + (d.bg[s] > 0 && a[s - 1] > 0 ? d.bg[s] : 0);
        a.push(l)
    }

    function r(a, e, s) {
        var l = a[s - 1] < d.bh[s - 1] ? d.bh[s - 1] : d.championByTableRow[s] == o.champions[e] ? a[s - 1] - 1 : a[s - 1];
        a.push(l)
    }

    function calculateTurnMeterForChampion(a, e, s, l, n) {
        // a: Champion index (0 through 4)
        // e: Champion turn meter array per table row.  Champion base effective turn meter increase per turn is e[0]
        // s: 30% speed boost array
        // l: 15% speed boost array
        // n: Table row number (2 - 444)
        var i =
            ("Kymar" != o.champions[a] && "Kymar" != d.championByTableRow[n - 1] ? d.teamTurnMeterFillFromCurrentSkillByTableRow[n - 1] : 0) +
            (d.championByTableRow[n - 1] != o.champions[a] ? e[n - 1] : 0) + // If this champion just went, their turn meter is 0.  If not, their turn meter is whatever it was before
            (s[n - 1] > 0 ? 1.3 * e[0] : l[n - 1] > 0 ? 1.15 * e[0] : e[0]) +
            ("Juliana" == o.champions[a] ? d.julianaPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Ultimate Galek" == o.champions[a] ? d.ultimateGalekPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Painkeeper" == o.champions[a] ? d.painkeeperPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Sikara" == o.champions[a] ? d.sikaraPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Valla" == o.champions[a] ? d.vallaPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Vrask" == o.champions[a] ? d.vraskPercentTurnMeterFillByTableRow[n - 1] : 0) +
            ("Hellgazer" == o.champions[a] ? d.hellgazerPercentTurnMeterFillByTableRow[n - 1] : 0) +
            (0 == a && "Foli" == o.champions[a] && "STUN" == d.championSkillByTableRow[n - 1] ? 0.5 : 0);
        e.push(i)
    }

    function _(a, e, s) {
        // a: champion-based array
        // e: champion index (aligns with array)
        // s: b from the for loop (1 - 444)
        var l =
            (a[s - 1] < 2 &&
                (
                    ("Turvold" == o.champions[e] && "Turvold" == d.championByTableRow[s] && "a3" == d.championSkillByTableRow[s]) ||
                    ("Hordin" == o.champions[e] && "Hordin" == d.championByTableRow[s] && "a3" == d.championSkillByTableRow[s])
                ) ? 1 :
                a[s - 1] < 3 &&
                    (
                        ("Hexia" == d.championByTableRow[s] && "Hexia" == o.champions[e] && "a3" == d.championSkillByTableRow[s]) ||
                        ("Elder Skarg" == o.champions[e] && "Elder Skarg" == d.championByTableRow[s] && "a1" == d.championSkillByTableRow[s]) ||
                        ("Galek" == o.champions[e] && "Galek" == d.championByTableRow[s] && "a2" == d.championSkillByTableRow[s])
                    ) ? 2 :
                    "Krisk" == o.champions[e] && "Krisk" == d.championByTableRow[s] && "a3" == d.championSkillByTableRow[s]
                        ? a[s - 1] - 1 :
                        d.bf[s] >= a[s - 1] ? d.bf[s] :
                            d.championByTableRow[s] == o.champions[e] ? a[s - 1] - 1 :
                                a[s - 1]) + (a[s - 1] > 0 ? d.bg[s] :
                                    0)
            - ("MaShalled" == d.championByTableRow[s] && "MaShalled" == o.champions[e] && "a3" == d.championSkillByTableRow[s] ? 1 : 0);
        a.push(l)
    }

    for (var m = 0, b = 1; b < 444; b++) {
        b > 1 &&
            (
                calculateTurnMeterForChampion(0, d.champ0TurnMeter, d.champ0BigSpeedBoostByTableRow, d.champ0SmallSpeedBoostByTableRow, b),
                calculateTurnMeterForChampion(1, d.champ1TurnMeter, d.champ1BigSpeedBoostByTableRow, d.champ1SmallSpeedBoostByTableRow, b),
                calculateTurnMeterForChampion(2, d.champ2TurnMeter, d.champ2BigSpeedBoostByTableRow, d.champ2SmallSpeedBoostByTableRow, b),
                calculateTurnMeterForChampion(3, d.champ3TurnMeter, d.champ3BigSpeedBoostByTableRow, d.champ3SmallSpeedBoostByTableRow, b),
                calculateTurnMeterForChampion(4, d.champ4TurnMeter, d.champ4BigSpeedBoostByTableRow, d.champ4SmallSpeedBoostByTableRow, b),
                d.clanBossTurnMeter.push(d.championByTableRow[b - 1] == o.boss ? d.clanBossTurnMeter[0] : d.clanBossTurnMeter[b - 1] + d.clanBossTurnMeter[0])
            );

        var maxTurnMeterForRound = Math.max(d.clanBossTurnMeter[b], d.champ0TurnMeter[b], d.champ1TurnMeter[b], d.champ2TurnMeter[b], d.champ3TurnMeter[b], d.champ4TurnMeter[b]);

        1 <= maxTurnMeterForRound ?
            d.champ0TurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.champions[0]) :
                d.champ1TurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.champions[1]) :
                    d.champ2TurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.champions[2]) :
                        d.champ3TurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.champions[3]) :
                            d.champ4TurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.champions[4]) :
                                d.clanBossTurnMeter[b] == maxTurnMeterForRound ? d.championByTableRow.push(o.boss) :
                                    d.championByTableRow.push("null") :
            d.championByTableRow.push("null"),

            t(0, 0, d.e, d.f, d.g, d.h, b),
            t(1, 1, d.j, d.k, d.l, d.m, b),
            t(2, 2, d.o, d.p, d.q, d.r, b),
            t(3, 3, d.t, d.u, d.v, d.w, b),
            t(4, 4, d.y, d.z, d.aa, d.ab, b),

            b > 4 && d.a.push(d.clanBossTurnMeter[b] == d.clanBossTurnMeter[0] ? d.a[b - 1] + 1 == 4 ? 1 : d.a[b - 1] + 1 : d.a[b - 1]),
            b > 1 && d.c.push(d.championByTableRow[b] == o.boss ? 1 == d.a[b] ? "AOE 1" : 2 == d.a[b] ? "AOE 2" : "STUN" : "null"),
            d.championByTableRow[b] == o.boss ?
                d.championSkillByTableRow.push(d.c[b]) : d.championByTableRow[b] == o.champions[0] ?
                    d.championSkillByTableRow.push(d.h[b]) : d.championByTableRow[b] == o.champions[1] ?
                        d.championSkillByTableRow.push(d.m[b]) : d.championByTableRow[b] == o.champions[2] ?
                            d.championSkillByTableRow.push(d.r[b]) : d.championByTableRow[b] == o.champions[3] ?
                                d.championSkillByTableRow.push(d.w[b]) : d.championByTableRow[b] == o.champions[4] ?
                                    d.championSkillByTableRow.push(d.ab[b]) : d.championSkillByTableRow.push("null");

        var percentTeamTurnMeterFillFromCurrentSkill = (("Arbiter" == d.championByTableRow[b] && ("a3" == d.championSkillByTableRow[b] || "a4" == d.championSkillByTableRow[b])) ||
            ("Lanakis" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) ||
            ("Lyssandra" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b]) ||
            ("Seeker" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) ? 0.3 : 0)
            + (("Shatterbones" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b]) ||
                (("AOE 1" == d.championSkillByTableRow[b] || "AOE 2" == d.championSkillByTableRow[b] || ("STUN" == d.championSkillByTableRow[b] && "Maulie" == o.champions[0])) &&
                    ("Maulie" == o.champions[0] ||
                        "Maulie" == o.champions[1] ||
                        "Maulie" == o.champions[2] ||
                        "Maulie" == o.champions[3] ||
                        "Maulie" == o.champions[4])) ? 0.25 : 0) +
            ((("Golden Reaper" == d.championByTableRow[b] || "Kymar" == d.championByTableRow[b]) && "a3" == d.championSkillByTableRow[b]) || ("Marksman" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) ? 0.2 : 0) +
            (("Siphi" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) ? 0.1 : 0) +
            ((("Haruspex" == d.championByTableRow[b] || "Sethallia" == d.championByTableRow[b] || "Gorgorab" == d.championByTableRow[b] || "Pitiless" == d.championByTableRow[b]) && "a2" == d.championSkillByTableRow[b]) || (("High Khatun" == d.championByTableRow[b] || "Apothecary" == d.championByTableRow[b]) && "a3" == d.championSkillByTableRow[b]) ? 0.15 : 0) +
            ((("Sethallia" != d.championByTableRow[b] && "Shirimani" != d.championByTableRow[b] && "Hellgazer" != d.championByTableRow[b]) || "a3" != d.championSkillByTableRow[b]) ? 0 : 0.1);

            d.teamTurnMeterFillFromCurrentSkillByTableRow.push(percentTeamTurnMeterFillFromCurrentSkill),

            d.ag.push("Painkeeper" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b] ? 1 : 0),
            setPercentTurnMeterFillFromA1OnTableRowInArray("Sikara", 0.25, d.sikaraPercentTurnMeterFillByTableRow, b),
            setPercentTurnMeterFillFromA1OnTableRowInArray("Painkeeper", 0.1, d.painkeeperPercentTurnMeterFillByTableRow, b),
            setPercentTurnMeterFillFromA1OnTableRowInArray("Ultimate Galek", 0.1, d.ultimateGalekPercentTurnMeterFillByTableRow, b),
            "Valla" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b] ? d.vallaPercentTurnMeterFillByTableRow.push(0.5) : d.vallaPercentTurnMeterFillByTableRow.push(0),
            set20PctTurnMeterFillByTableRow("Vrask", d.vraskPercentTurnMeterFillByTableRow, b),
            set20PctTurnMeterFillByTableRow("Juliana", d.julianaPercentTurnMeterFillByTableRow, b),
            "Hellgazer" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b] ? d.hellgazerPercentTurnMeterFillByTableRow.push(0.1) : d.hellgazerPercentTurnMeterFillByTableRow.push(0),
            ("Broadmaw" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) || ("Haruspex" == d.championByTableRow[b] && "a2" == d.championSkillByTableRow[b]) ? d.be.push(2) : "Hexweaver" == d.championByTableRow[b] && "a3" == d.championSkillByTableRow[b] ? d.be.push(2) : d.be.push(0), ("Apothecary" != d.championByTableRow[b] && "High Khatun" != d.championByTableRow[b] && "Lyssandra" != d.championByTableRow[b] && "Krisk" != d.championByTableRow[b] && "Golden Reaper" != d.championByTableRow[b] && "Hexia" != d.championByTableRow[b] && "Diabolist" != d.championByTableRow[b] && "Frostbringer" != d.championByTableRow[b] && "MaShalled" != d.championByTableRow[b] && "Shamrock" != d.championByTableRow[b]) || "a3" !== d.championSkillByTableRow[b] ? "a2" == (("Siphi" == d.championByTableRow[b] || "Heiress" == d.championByTableRow[b]) && d.championSkillByTableRow[b]) ? d.bf.push(2) : d.bf.push(0) : d.bf.push(2),
            ("Krisk" != d.championByTableRow[b] && "Hope" != d.championByTableRow[b] && "Flesh-Tearer" != d.championByTableRow[b] && "Valerie" != d.championByTableRow[b] && "Sandlashed Survivor" != d.championByTableRow[b] && "Lanakis" != d.championByTableRow[b]) || "a2" != d.championSkillByTableRow[b] ? d.bg.push(0) : d.bg.push(1),
            ("Martyr" != d.championByTableRow[b] && "Valkyrie" != d.championByTableRow[b] && "Skullcrusher" != d.championByTableRow[b]) || "a2" != d.championSkillByTableRow[b] ? d.bh.push(0) : d.bh.push(2),
            u(d.champ0SmallSpeedBoostByTableRow, 0, b),
            u(d.champ1SmallSpeedBoostByTableRow, 1, b),
            u(d.champ2SmallSpeedBoostByTableRow, 2, b),
            u(d.champ3SmallSpeedBoostByTableRow, 3, b),
            u(d.champ4SmallSpeedBoostByTableRow, 4, b),
            r(d.ar, 0, b),
            r(d.au, 1, b),
            r(d.ax, 2, b),
            r(d.ba, 3, b),
            r(d.bd, 4, b),
            _(d.champ0BigSpeedBoostByTableRow, 0, b),
            _(d.champ1BigSpeedBoostByTableRow, 1, b),
            _(d.champ2BigSpeedBoostByTableRow, 2, b),
            _(d.champ3BigSpeedBoostByTableRow, 3, b),
            _(d.champ4BigSpeedBoostByTableRow, 4, b),
            d.clanBossTurnByTableRow.push(d.championByTableRow[b] == o.boss ? d.clanBossTurnByTableRow[b - 1] + 1 : d.clanBossTurnByTableRow[b - 1]),
            "null" != d.championByTableRow[b] &&
        ("STUN" == d.championSkillByTableRow[b] && (a = "c5"),
            "AOE 2" == d.championSkillByTableRow[b] && (a = "b5"),
            "AOE 1" == d.championSkillByTableRow[b] && (a = "a5"),
            "a1" == d.championSkillByTableRow[b] && (a = "h1"),
            "a2" == d.championSkillByTableRow[b] && (a = "g1"),
            "a3" == d.championSkillByTableRow[b] && (a = "f1"),
            "a4" == d.championSkillByTableRow[b] && (a = "e1"),
            (e = d.championByTableRow[b] == o.boss ? "i15" : "trans"),
            d.championByTableRow[b] == o.champions[4] && (e = "f73"),
            d.championByTableRow[b] == o.champions[3] && (e = "f59"),
            d.championByTableRow[b] == o.champions[2] && (e = "f45"),
            d.championByTableRow[b] == o.champions[1] && (e = "f31"),
            d.championByTableRow[b] == o.champions[0] && (e = "f17"),
            (i += '<tr><td style="background:rgb(239, 96, 84, ' + (0 + m) + ');">' + d.clanBossTurnByTableRow[b] + '</td><td class="' + e + '">' + d.championByTableRow[b] + '</td><td class="' + a + '">' + d.championSkillByTableRow[b] + "</td></tr>"),
            (m += 0.05),
            (long_[0] = d.clanBossTurnByTableRow[b]), // CB Turn Number
            (long_[1] = m),
            (long_[2] = e),
            (long_[3] = d.championByTableRow[b]), // Champion
            (long_[4] = a),
            (long_[5] = d.championSkillByTableRow[b]), // Skill used
            0)
    }

    if (long_[0] < 50)
        for (b = long_[0]; b <= 50; b++)
            i += '<tr><td style="background:rgb(239, 96, 84, ' + (0 + long_[1]) + ');">' + long_[0] + '</td><td class="' + long_[2] + '">' + long_[3] + '</td><td class="' + long_[4] + '">' + long_[5] + "</td></tr>";
            $("#table").append(i),
            $("#boss_speed").html(o.difficulty_val)
}
