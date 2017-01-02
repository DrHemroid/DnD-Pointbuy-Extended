using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnD_Point_Buy
{
    public partial class Form1 : Form
    {
        int points_total;
        int points_remaining;
        //note to self: next time, use an array!
        int stat_str, stat_dex, stat_con, stat_int, stat_wis, stat_cha, stat_hon, stat_san;
        int mod_str, mod_dex, mod_con, mod_int, mod_wis, mod_cha, mod_hon, mod_san;
        int bonus_str, bonus_dex, bonus_con, bonus_int, bonus_wis, bonus_cha, bonus_hon, bonus_san;
        int cost_str, cost_dex, cost_con, cost_int, cost_wis, cost_cha, cost_hon, cost_san;

        public Form1()
        {
            InitializeComponent();
            this.ui_bonus_hon.ValueChanged += new System.EventHandler(this.ui_bonus_hon_ValueChanged);
            this.ui_stat_hon.ValueChanged += new System.EventHandler(this.ui_stat_hon_ValueChanged);
            this.ui_bonus_san.ValueChanged += new System.EventHandler(this.ui_bonus_san_ValueChanged);
            this.ui_stat_san.ValueChanged += new System.EventHandler(this.ui_stat_san_ValueChanged);
            UpdateAllVariables();
            UpdateAllUI();
        }
        public void UpdateAllUI(){
            //updates all ui (text boxes) to the calculated variables
            ui_mod_str.Text = mod_str.ToString("+#;-#;+0");
            ui_mod_dex.Text = mod_dex.ToString("+#;-#;+0");
            ui_mod_con.Text = mod_con.ToString("+#;-#;+0");
            ui_mod_int.Text = mod_int.ToString("+#;-#;+0");
            ui_mod_wis.Text = mod_wis.ToString("+#;-#;+0");
            ui_mod_cha.Text = mod_cha.ToString("+#;-#;+0");
            ui_mod_hon.Text = mod_hon.ToString("+#;-#;+0");
            ui_mod_san.Text = mod_san.ToString("+#;-#;+0");
            ui_cost_str.Text = cost_str.ToString();
            ui_cost_dex.Text = cost_dex.ToString();
            ui_cost_con.Text = cost_con.ToString();
            ui_cost_int.Text = cost_int.ToString();
            ui_cost_wis.Text = cost_wis.ToString();
            ui_cost_cha.Text = cost_cha.ToString();
            ui_cost_hon.Text = cost_hon.ToString();
            ui_cost_san.Text = cost_san.ToString();
            ui_points_remaining.Text = points_remaining.ToString();
            ui_score_str.Text = (stat_str + bonus_str).ToString();
            ui_score_dex.Text = (stat_dex + bonus_dex).ToString();
            ui_score_con.Text = (stat_con + bonus_con).ToString();
            ui_score_int.Text = (stat_int + bonus_int).ToString();
            ui_score_wis.Text = (stat_wis + bonus_wis).ToString();
            ui_score_cha.Text = (stat_cha + bonus_cha).ToString();
            ui_score_hon.Text = (stat_hon + bonus_hon).ToString();
            ui_score_san.Text = (stat_san + bonus_san).ToString();
        }
        public void UpdateAllVariables(){
            //updates all variables based on the ui values
            //calculates mod and stat_cost based on stat values
            //calculated remaining cost too
            //never perform before validating the ui values
            points_total = Convert.ToInt32(ui_points_total.Value);
            stat_str = Convert.ToInt32(ui_stat_str.Value);
            stat_dex = Convert.ToInt32(ui_stat_dex.Value);
            stat_con = Convert.ToInt32(ui_stat_con.Value);
            stat_int = Convert.ToInt32(ui_stat_int.Value);
            stat_wis = Convert.ToInt32(ui_stat_wis.Value);
            stat_cha = Convert.ToInt32(ui_stat_cha.Value);
            stat_hon = Convert.ToInt32(ui_stat_hon.Value);
            stat_san = Convert.ToInt32(ui_stat_san.Value);
            bonus_str = Convert.ToInt32(ui_bonus_str.Value);
            bonus_dex = Convert.ToInt32(ui_bonus_dex.Value);
            bonus_con = Convert.ToInt32(ui_bonus_con.Value);
            bonus_int = Convert.ToInt32(ui_bonus_int.Value);
            bonus_wis = Convert.ToInt32(ui_bonus_wis.Value);
            bonus_cha = Convert.ToInt32(ui_bonus_cha.Value);
            bonus_hon = Convert.ToInt32(ui_bonus_hon.Value);
            bonus_san = Convert.ToInt32(ui_bonus_san.Value);
            UpdateCalculations();
        }

        private int CalcCost(int stat){
            int cost = 0;
            if (stat <= 13)
            {
                cost = stat - 8;
            }
            else {
                cost = CalcMod(stat) + CalcCost(stat - 1);
            }
            return cost;
        }

        private int CalcMod(int stat){
            return (int)Math.Floor((stat-10.0)/2);
        }

        private void UpdateCalculations()
        {
            mod_str = CalcMod(stat_str+bonus_str);
            mod_dex = CalcMod(stat_dex + bonus_dex);
            mod_con = CalcMod(stat_con + bonus_con);
            mod_int = CalcMod(stat_int + bonus_int);
            mod_wis = CalcMod(stat_wis + bonus_wis);
            mod_cha = CalcMod(stat_cha + bonus_cha);
            mod_hon = CalcMod(stat_hon + bonus_hon);
            mod_san = CalcMod(stat_san + bonus_san);
            cost_str = CalcCost(stat_str);
            cost_dex = CalcCost(stat_dex);
            cost_con = CalcCost(stat_con);
            cost_int = CalcCost(stat_int);
            cost_wis = CalcCost(stat_wis);
            cost_cha = CalcCost(stat_cha);
            cost_hon = CalcCost(stat_hon);
            cost_san = CalcCost(stat_san);
            points_remaining = points_total - cost_str - cost_dex - cost_con - cost_int - cost_wis - cost_cha - cost_hon - cost_san;
        }

        private void ValidateChange(int newStat, int oldStat, int pointsRemaining, NumericUpDown ui)
        {
            //Will check if the stat exceeds cost, and set it to old value if it does.
            if (newStat > oldStat)
            {
                if (CalcCost(oldStat + 1) - CalcCost(oldStat) > pointsRemaining)
                {
                    ui.Value = oldStat;
                }
            }
        }

        private void ui_stat_str_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_str.Value, stat_str, points_remaining, ui_stat_str);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_dex_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_dex.Value, stat_dex, points_remaining, ui_stat_dex);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_con_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_con.Value, stat_con, points_remaining, ui_stat_con);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_int_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_int.Value, stat_int, points_remaining, ui_stat_int);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_wis_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_wis.Value, stat_wis, points_remaining, ui_stat_wis);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_cha_ValueChanged(object sender, EventArgs e)
        {
            ValidateChange((int)ui_stat_cha.Value, stat_cha, points_remaining, ui_stat_cha);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_hon_ValueChanged(object sender, EventArgs e) {
            ValidateChange((int)ui_stat_hon.Value, stat_hon, points_remaining, ui_stat_hon);
            UpdateAllVariables();
            UpdateAllUI();
        }
        private void ui_stat_san_ValueChanged(object sender, EventArgs e) {
            ValidateChange((int)ui_stat_san.Value, stat_san, points_remaining, ui_stat_san);
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_str_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_dex_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_con_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_int_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_wis_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_cha_ValueChanged(object sender, EventArgs e)
        {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_hon_ValueChanged(object sender, EventArgs e) {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_bonus_san_ValueChanged(object sender, EventArgs e) {
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void ui_points_total_ValueChanged(object sender, EventArgs e)
        {
            //custom version of ValidateChange()
            if (Convert.ToInt32(ui_points_total.Value) < points_total)
            {
                if (points_total - 1 < points_total - points_remaining)
                {
                    ui_points_total.Value = points_total;
                }
            }
            UpdateAllVariables();
            UpdateAllUI();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetVariables();
            ui_rule_extended.Checked = true;
            ui_rule_minmax.Checked = false;
            ui_rule_standard.Checked = false;
        }

        public void ResetVariables()
        {
            points_total = 30;
            points_remaining = 30;
            stat_str = 8;
            stat_dex = 8;
            stat_con = 8;
            stat_int = 8;
            stat_wis = 8;
            stat_cha = 8;
            stat_hon = 8;
            stat_san = 8;
            bonus_str = 0;
            bonus_dex = 0;
            bonus_con = 0;
            bonus_int = 0;
            bonus_wis = 0;
            bonus_cha = 0;
            bonus_hon = 0;
            bonus_san = 0;
            ui_points_total.Value = 30;
            ui_stat_str.Value = 8;
            ui_stat_dex.Value = 8;
            ui_stat_con.Value = 8;
            ui_stat_int.Value = 8;
            ui_stat_wis.Value = 8;
            ui_stat_cha.Value = 8;
            ui_stat_hon.Value = 8;
            ui_stat_san.Value = 8;
            ui_bonus_str.Value = 0;
            ui_bonus_dex.Value = 0;
            ui_bonus_con.Value = 0;
            ui_bonus_int.Value = 0;
            ui_bonus_wis.Value = 0;
            ui_bonus_cha.Value = 0;
            ui_bonus_hon.Value = 0;
            ui_bonus_san.Value = 0;
            UpdateCalculations();
            UpdateAllUI();
        }

        private void SetMinimumStats(int n)
        {
            ui_stat_str.Minimum = n;
            ui_stat_dex.Minimum = n;
            ui_stat_con.Minimum = n;
            ui_stat_int.Minimum = n;
            ui_stat_wis.Minimum = n;
            ui_stat_cha.Minimum = n;
            ui_stat_hon.Minimum = n;
            ui_stat_san.Minimum = n;
        }

        private void SetMaximumStats(int n)
        {
            ui_stat_str.Maximum = n;
            ui_stat_dex.Maximum = n;
            ui_stat_con.Maximum = n;
            ui_stat_int.Maximum = n;
            ui_stat_wis.Maximum = n;
            ui_stat_cha.Maximum = n;
            ui_stat_hon.Maximum = n;
            ui_stat_san.Maximum = n;
        }

        private void ui_rule_standard_CheckedChanged(object sender, EventArgs e)
        {
            if (ui_rule_standard.Checked == true)
            {
                ResetVariables();
                SetMinimumStats(8);
                SetMaximumStats(15);
                ui_points_total.Value = 27;
                ui_points_total.Enabled = false;
            }

        }

        private void ui_rule_extended_CheckedChanged(object sender, EventArgs e)
        {
            if (ui_rule_extended.Checked == true)
            {
                ResetVariables();
                SetMinimumStats(6);
                SetMaximumStats(18);
                ui_points_total.Enabled = true;
            }
        }

        private void ui_rule_minmax_CheckedChanged(object sender, EventArgs e)
        {
            if (ui_rule_minmax.Checked == true)
            {
                ResetVariables();
                SetMinimumStats(3);
                SetMaximumStats(20);
                ui_points_total.Enabled = true;
                ui_points_total.Minimum = 0;
                ui_points_total.Maximum = 168;
                ui_bonus_str.Minimum = -10;
                ui_bonus_dex.Minimum = -10;
                ui_bonus_con.Minimum = -10;
                ui_bonus_int.Minimum = -10;
                ui_bonus_wis.Minimum = -10;
                ui_bonus_cha.Minimum = -10;
                ui_bonus_hon.Minimum = -10;
                ui_bonus_san.Minimum = -10;
                ui_bonus_str.Maximum = 10;
                ui_bonus_dex.Maximum = 10;
                ui_bonus_con.Maximum = 10;
                ui_bonus_int.Maximum = 10;
                ui_bonus_wis.Maximum = 10;
                ui_bonus_cha.Maximum = 10;
                ui_bonus_hon.Maximum = 10;
                ui_bonus_san.Maximum = 10;
            }
        }



    }
}
